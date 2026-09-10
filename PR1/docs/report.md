# Smart Phone Money Recognition (PR1)

## Abstract
The goal of this project is to design and develop an Android application capable of recognizing different denominations of paper currency. By extracting specific visual features and training a recognition algorithm using the OpenCV library, the system identifies the currency and provides audio feedback (Text-to-Speech) to announce the value.

This application is primarily designed to assist visually impaired individuals. According to the World Health Organization, approximately 284 million people worldwide suffer from vision impairment, including 39 million who are totally blind. For these individuals, distinguishing between banknotes of similar size or color can be a significant daily challenge, especially under varying lighting conditions or when the banknote is folded.

---

## Chapter 1: Introduction

### 1.1 General Introduction
Operating systems act as the intermediary between the user and the hardware. With the evolution of smart devices, mobile operating systems have combined the power of personal computers with the portability of handheld devices. Android, an open-source platform based on the Linux kernel, has become the most dominant system due to its flexibility for developers and wide adoption by manufacturers.

### 1.2 Project Objectives
The primary objective is to create an Android-based tool that:
1. Extracts unique visual features from banknotes.
2. Uses a trained computer vision algorithm to classify the denomination.
3. Converts the result into speech to assist the user.

### 1.3 Project Concept
Currency recognition is a complex task because banknotes often have similar dimensions and color palettes. Environmental factors, such as lighting and the orientation of the note, can lead to detection failures. This project aims to overcome these obstacles by focusing on distinct geometric features of the currency.

---

## Chapter 2: The Android Platform

### 2.1 Android Overview
Android is an open-source mobile operating system based on the Linux kernel. It was developed to provide a flexible and scalable platform for touch-screen devices. The Android architecture consists of several layers:
- **Application Layer:** The top layer where user apps reside.
- **Application Framework:** Provides high-level services to apps.
- **Libraries:** Includes core C/C++ libraries, including the OpenCV library used in this project.
- **Android Runtime:** Includes the Dalvik Virtual Machine (DVM) / ART.
- **Linux Kernel:** Manages hardware drivers and power management.

### 2.2 Key Features
- **Storage:** Uses SQLite for local data storage.
- **Connectivity:** Supports GSM, LTE, Wi-Fi, Bluetooth, and NFC.
- **Open Source:** Allows developers to modify the system and integrate native C++ code via the NDK.

---

## Chapter 3: Analysis Phase

### 3.1 Development Methodology
The project followed the **Waterfall Model (Linear Sequential)**, progressing through the following stages:
Requirements Gathering $\rightarrow$ Analysis $\rightarrow$ Design $\rightarrow$ Development $\rightarrow$ Deployment.

### 3.2 Requirements
- **Hardware:** Any Android device with a camera (Android 4.0+).
- **Functional Requirements:**
    - Real-time camera feed processing.
    - Feature extraction and classification.
    - Audio output of the recognized value.
- **Non-Functional Requirements:**
    - **Reliability:** High accuracy in detection to reduce human error.
    - **Response Time:** Low latency for real-time feedback.
    - **Usability:** Simple interface for visually impaired users.

---

## Chapter 4: Design and Implementation

### 4.1 System Design
The application acts as a bridge between the Android Camera API and the OpenCV library. It processes the camera feed as a sequence of frames.

### 4.2 Technical Implementation
- **Cascade Classifier:** The system uses a **Haar-like feature** based Cascade Classifier trained with the **AdaBoost** algorithm.
- **Feature Selection:** The algorithm was trained to recognize the **octagonal shape** found on the 500 Syrian Pound note, as this geometric feature is distinct and reliable.
- **Native Integration (JNI):** To ensure high performance, the core detection logic was written in **C++** and integrated into the Java application using the **Java Native Interface (JNI)** and the **Android NDK**.

### 4.3 Core Classes
- **`Money`**: The main activity that manages the camera view and coordinates the detection process.
- **`DetectionBasedTracker`**: A bridge class connecting the Java UI to the native C++ detection logic.
- **`CascadeDetectorAdapter`**: A C++ class that implements the actual `detectMultiScale` function from OpenCV to locate the target object.

### 4.4 Key OpenCV Functions
- **`detectMultiScale()`**: Used to build an image pyramid and run a sliding window detector to find the target object at various scales.
- **`Imgproc.rectangle()`**: Used to draw a bounding box around the detected currency feature for visual verification.

---

## Chapter 5: Testing and Challenges

### 5.1 Testing Results
The system was tested using a real Android device. When a 500 SYP note is presented, the system identifies the octagonal feature and draws a green bounding box around it. The accuracy increases as the "Minimum Box Size" is adjusted via the app settings.

### 5.2 Challenges Encountered
1. **Environment Integration:** Integrating the OpenCV SDK with Android Studio and configuring the NDK/JNI toolchain was a significant technical challenge.
2. **Library Limitations:** Some advanced color-based feature extraction algorithms were removed from the free version of the library, forcing a shift toward geometric (Haar) features.
3. **Hardware Dependency:** Testing required a physical device with a camera, as the Android Emulator (AVD) could not simulate the necessary camera feed.

---

## Chapter 6: Conclusion and Future Work

### 6.1 Conclusion
This project demonstrates the power of combining mobile platforms with computer vision to solve real-world accessibility problems. Despite the challenges of lighting and image quality, the system provides a functional proof-of-concept for currency recognition.

### 6.2 Future Work
- **Deep Learning:** Replace Cascade Classifiers with Convolutional Neural Networks (CNNs) for significantly higher accuracy.
- **Counterfeit Detection:** Implement security feature analysis to detect fake banknotes.
- **Self-Learning:** Allow the app to learn new currency denominations through user-provided samples.
