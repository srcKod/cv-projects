package pr1.svu.com.money;

import org.opencv.core.Mat;
import org.opencv.core.MatOfRect;


public class DetectionBasedTracker {


        public DetectionBasedTracker(String cascadeName, int minBoxSize) {
            mNativeObj = nativeCreateObject(cascadeName, minBoxSize);
        }

        public void start() {
            nativeStart(mNativeObj);
        }

        public void stop() {
            nativeStop(mNativeObj);
        }

        public void setMinBoxSize(int size) {
            nativeSetBoxSize(mNativeObj, size);
        }

        public void detect(Mat imageGray, MatOfRect Boxes) {
            nativeDetect(mNativeObj, imageGray.getNativeObjAddr(), Boxes.getNativeObjAddr());
        }

        public void release() {
            nativeDestroyObject(mNativeObj);
            mNativeObj = 0;
        }

        private long mNativeObj = 0;

        private static native long nativeCreateObject(String cascadeName, int minFaceSize);
        private static native void nativeDestroyObject(long thiz);
        private static native void nativeStart(long thiz);
        private static native void nativeStop(long thiz);
        private static native void nativeSetBoxSize(long thiz, int size);
        private static native void nativeDetect(long thiz, long inputImage, long Boxes);
    }

