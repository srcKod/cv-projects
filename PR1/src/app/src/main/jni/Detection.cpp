
#include "Detection.h"
#include <opencv2/core/core.hpp>
#include <opencv2/objdetect.hpp>
#include <android/log.h>

#define LOG_TAG "MoneyDetection/Detection"
#define LOGD(...) ((void)__android_log_print(ANDROID_LOG_DEBUG, LOG_TAG, __VA_ARGS__))

using namespace std;
using namespace cv;

/*inline void vector_Rect_to_Mat(vector<Rect>& v_rect, Mat& mat)
{
    mat = Mat(v_rect, true);
}*/

class CascadeDetectorAdapter: public DetectionBasedTracker::IDetector
{
public:
    CascadeDetectorAdapter(Ptr<CascadeClassifier> detector):
            IDetector(),
            Detector(detector)
    {
        LOGD("CascadeDetectorAdapter::Detect::Detect");
        CV_Assert(detector);
    }

    void detect(const Mat &Image, vector<Rect> &objects)
    {
        LOGD("CascadeDetectorAdapter::Detect: begin");
        LOGD("CascadeDetectorAdapter::Detect: scaleFactor=%.2f, minNeighbours=%d, minObjSize=(%dx%d), maxObjSize=(%dx%d)",
             scaleFactor, minNeighbours, minObjSize.width, minObjSize.height, maxObjSize.width, maxObjSize.height);

        Detector->detectMultiScale(Image, objects, scaleFactor, minNeighbours, 0, minObjSize, maxObjSize);
        LOGD("CascadeDetectorAdapter::Detect: end");
    }

    virtual ~CascadeDetectorAdapter()
    {
        LOGD("CascadeDetectorAdapter::Detect::~Detect");
    }

private:
    CascadeDetectorAdapter();
    Ptr<CascadeClassifier> Detector;
};

struct DetectorAgregator
{
    Ptr<CascadeDetectorAdapter> mainDetector;
    Ptr<CascadeDetectorAdapter> trackingDetector;

    Ptr<DetectionBasedTracker> tracker;
    DetectorAgregator(Ptr<CascadeDetectorAdapter>& _mainDetector, Ptr<CascadeDetectorAdapter>& _trackingDetector):
            mainDetector(_mainDetector),
            trackingDetector(_trackingDetector)
    {
        CV_Assert(_mainDetector);
        CV_Assert(_trackingDetector);

        DetectionBasedTracker::Parameters DetectorParams;
        tracker = makePtr<DetectionBasedTracker>(mainDetector, trackingDetector, DetectorParams);
    }
};

JNIEXPORT jlong JNICALL
  Java_pr1_svu_com_money_DetectionBasedTracker_nativeCreateObject
      (JNIEnv * jenv, jclass,jstring jFileName, jint BoxSize)
{
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeCreateObject enter");
    const char* jnamestr = jenv->GetStringUTFChars(jFileName, NULL);
    string stdFileName(jnamestr);
    jlong result = 0;

    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeCreateObject");

    try
    {
        Ptr<CascadeDetectorAdapter> mainDetector = makePtr<CascadeDetectorAdapter>(
            makePtr<CascadeClassifier>(stdFileName));
        Ptr<CascadeDetectorAdapter> trackingDetector = makePtr<CascadeDetectorAdapter>(
            makePtr<CascadeClassifier>(stdFileName));
        result = (jlong)new DetectorAgregator(mainDetector, trackingDetector);
        if (BoxSize > 0)
        {
            mainDetector->setMinObjectSize(Size(BoxSize, BoxSize));
            //trackingDetector->setMinObjectSize(Size(faceSize, faceSize));
        }
    }
    catch(Exception& e)
    {
        LOGD("nativeCreateObject caught cv::Exception: %s", e.what());
        jclass je = jenv->FindClass("org/opencv/core/CvException");
        if(!je)
            je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, e.what());
    }
        catch (...)
        {
        LOGD("nativeCreateObject caught unknown exception");
        jclass je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, "Unknown exception in JNI code of DetectionBasedTracker.nativeCreateObject()");
        return 0;
    }

    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeCreateObject exit");
    return result;
}

JNIEXPORT void JNICALL
  Java_pr1_svu_com_money_DetectionBasedTracker_nativeDestroyObject
     (JNIEnv * jenv, jclass, jlong thiz)
{
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeDestroyObject");

    try
    {
        if(thiz != 0)
        {
            ((DetectorAgregator*)thiz)->tracker->stop();
            delete (DetectorAgregator*)thiz;
        }
    }
    catch(Exception& e)
    {
        LOGD("nativeestroyObject caught Exception: %s", e.what());
        jclass je = jenv->FindClass("org/opencv/core/CvException");
        if(!je)
            je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, e.what());
    }
    catch (...)
    {
        LOGD("nativeDestroyObject caught unknown exception");
        jclass je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, "Unknown exception in JNI code of DetectionBasedTracker.nativeDestroyObject()");
    }
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeDestroyObject exit");
}

JNIEXPORT void JNICALL
  Java_pr1_svu_com_money_DetectionBasedTracker_nativeStart
    (JNIEnv * jenv, jclass, jlong thiz)
{
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeStart");

    try
    {
        ((DetectorAgregator*)thiz)->tracker->run();
    }
    catch(Exception& e)
    {
        LOGD("nativeStart caught Exception: %s", e.what());
        jclass je = jenv->FindClass("org/opencv/core/CvException");
        if(!je)
            je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, e.what());
    }
    catch (...)
    {
        LOGD("nativeStart caught unknown exception");
        jclass je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, "Unknown exception in JNI code of DetectionBasedTracker.nativeStart()");
    }
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeStart exit");
}

JNIEXPORT void JNICALL
  Java_pr1_svu_com_money_DetectionBasedTracker_nativeStop
     (JNIEnv * jenv, jclass, jlong thiz)
{
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeStop");

    try
    {
        ((DetectorAgregator*)thiz)->tracker->stop();
    }
    catch(cv::Exception& e)
    {
        LOGD("nativeStop caught cv::Exception: %s", e.what());
        jclass je = jenv->FindClass("org/opencv/core/CvException");
        if(!je)
            je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, e.what());
    }
    catch (...)
    {
        LOGD("nativeStop caught unknown exception");
        jclass je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, "Unknown exception in JNI code of DetectionBasedTracker.nativeStop()");
    }
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeStop exit");
}

JNIEXPORT void JNICALL
  Java_pr1_svu_com_money_DetectionBasedTracker_nativeSetBoxSize
     (JNIEnv * jenv, jclass, jlong thiz, jint BoxSize)
{
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeSetBoxSize -- BEGIN");

    try
    {
        if (BoxSize > 0)
        {
            ((DetectorAgregator*)thiz)->mainDetector->setMinObjectSize(Size(BoxSize, BoxSize));
            //((DetectorAgregator*)thiz)->trackingDetector->setMinObjectSize(Size(faceSize, faceSize));
        }
    }
    catch(Exception& e)
    {
        LOGD("nativeStop caught cv::Exception: %s", e.what());
        jclass je = jenv->FindClass("org/opencv/core/CvException");
        if(!je)
            je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, e.what());
    }
    catch (...)
    {
        LOGD("nativeSetFaceSize caught unknown exception");
        jclass je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, "Unknown exception in JNI code of DetectionBasedTracker.nativeSetFaceSize()");
    }
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeSetBoxSize -- END");
}


JNIEXPORT void JNICALL
  Java_pr1_svu_com_money_DetectionBasedTracker_nativeDetect
    (JNIEnv * jenv, jclass, jlong thiz, jlong imageGray, jlong Boxes)
{
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeDetect");


    try
    {
        vector<Rect> RectBoxes;
        ((DetectorAgregator*)thiz)->tracker->process(*((Mat*)imageGray));
        ((DetectorAgregator*)thiz)->tracker->getObjects(RectBoxes);
        *((Mat*)Boxes) = Mat(RectBoxes, true);
    }
    catch(cv::Exception& e)
    {
        LOGD("nativeCreateObject caught cv::Exception: %s", e.what());
        jclass je = jenv->FindClass("org/opencv/core/CvException");
        if(!je)
            je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, e.what());
    }
    catch (...)
    {
        LOGD("nativeDetect caught unknown exception");
        jclass je = jenv->FindClass("java/lang/Exception");
        jenv->ThrowNew(je, "Unknown exception in JNI code DetectionBasedTracker.nativeDetect()");
    }
    LOGD("Java_pr1_svu_com_money_DetectionBasedTracker_nativeDetect END");
}
