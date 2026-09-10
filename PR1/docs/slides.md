# Presentation: Smart Phone Money Recognition (PR1)

## Slide 1: Title
**Project:** Smart Phone Money Recognition
**Institution:** Syrian Virtual University (SVU) - Information Engineering Program

---

## Slide 2: Introduction & Motivation
**The Concept:**
A mobile application that uses the camera to recognize paper currency and announce the value via audio.

**The Motivation:**
Assisting visually impaired individuals. According to the WHO, millions of people suffer from vision loss, making it difficult to distinguish between banknotes of similar size or color during daily transactions.

---

## Slide 3: Project Objectives
- Design an Android application for real-time currency recognition.
- Extract unique visual features using the OpenCV library.
- Implement a Text-to-Speech (TTS) system to announce the recognized value.
- Ensure the system is lightweight and runs on standard Android hardware.

---

## Slide 4: Technical Stack
- **Platform:** Android (Linux Kernel)
- **Languages:** Java (UI/Logic) and C++ (Native Processing)
- **Computer Vision:** OpenCV 3.1.0 Android SDK
- **Integration:** JNI (Java Native Interface) & Android NDK
- **IDE:** Android Studio 2.1
- **JDK:** Java Development Kit 8

---

## Slide 5: Development Environment Setup
- **OpenCV Integration:** Imported as a module and linked via `build.gradle`.
- **Native Code:** Configured the NDK to compile C++ code for multiple ABIs (armeabi-v7a, x86, mips).
- **Dependencies:** Configured `jcenter` and Maven repositories for Gradle builds.

---

## Slide 6: Implementation - The Algorithm
**Detection Method:**
- Used a **Cascade Classifier** based on the **AdaBoost** algorithm.
- **Haar-like Features:** The system scans the image using a sliding window to find specific patterns.
- **Target Feature:** The algorithm was trained to recognize the **octagonal shape** found on the 500 SYP note.
- **Training:** A custom XML classifier was created using a set of positive (containing the object) and negative (not containing the object) images.

---

## Slide 7: Software Architecture
- **`Money` Class:** Main activity; handles the camera feed and UI.
- **`DetectionBasedTracker`**: Bridge between Java and Native C++.
- **`CascadeDetectorAdapter`**: C++ class that executes the `detectMultiScale` function for high-performance detection.

---

## Slide 8: Key OpenCV Functions
- **`onCameraFrame`**: Captures frames in both RGBA and Grayscale.
- **`detectMultiScale`**: Builds an image pyramid to detect the target object at different scales.
- **`Imgproc.rectangle`**: Draws a bounding box around the detected currency for visual feedback.

---

## Slide 9: Testing & Results
- **Verification:** Tested on a physical Android device.
- **Outcome:** Successfully detected the 500 SYP note by identifying its octagonal feature.
- **Optimization:** Added a "Box Size" setting to allow users to adjust the detection sensitivity based on the distance of the note from the camera.

---

## Slide 10: Challenges
- **NDK/JNI Complexity:** Managing the bridge between Java and C++ and configuring the Gradle build for native libraries.
- **Hardware Constraints:** The need for a physical device for testing (AVD limitations).
- **Environmental Factors:** Lighting and image quality affecting the accuracy of Haar-like features.

---

## Slide 11: Conclusion & Future Work
**Conclusion:**
The project successfully demonstrates the integration of OpenCV with Android to create an accessibility tool.

**Future Work:**
- **Deep Learning:** Transition to CNNs (Convolutional Neural Networks) for better accuracy.
- **Anti-Counterfeiting:** Adding features to detect fake banknotes.
- **Expanded Dataset:** Training the system to recognize all denominations of multiple currencies.
