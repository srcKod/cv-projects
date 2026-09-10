#include <jni.h>

#ifndef _Included_pr1_svu_com_money_Money
#define _Included_pr1_svu_com_money_Money


#ifdef __cplusplus
extern "C" {
#endif


JNIEXPORT jlong JNICALL
 Java_pr1_svu_com_money_DetectionBasedTracker_nativeCreateObject
  (JNIEnv *, jclass, jstring, jint);


JNIEXPORT void JNICALL
 Java_pr1_svu_com_money_DetectionBasedTracker_nativeDestroyObject
  (JNIEnv *, jclass, jlong);


JNIEXPORT void JNICALL
 Java_pr1_svu_com_money_DetectionBasedTracker_nativeStart
  (JNIEnv *, jclass, jlong);


JNIEXPORT void JNICALL
 Java_pr1_svu_com_money_DetectionBasedTracker_nativeStop
  (JNIEnv *, jclass, jlong);


JNIEXPORT void JNICALL
 Java_pr1_svu_com_money_DetectionBasedTracker_nativeSetBoxSize
  (JNIEnv *, jclass, jlong, jint);


JNIEXPORT void JNICALL
 Java_pr1_svu_com_money_DetectionBasedTracker_nativeDetect
  (JNIEnv *, jclass, jlong, jlong, jlong);

#ifdef __cplusplus
}
#endif
#endif //MONEY_DETECTION_H
