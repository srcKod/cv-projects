# OMR Digital Correction System

## Abstract
Automatic Correction Systems based on Optical Mark Recognition (OMR) are widely used in universities and colleges to grade Multiple Choice Questions (MCQs). However, traditional systems often rely on expensive, specialized scanners and proprietary paper templates, which limit flexibility and increase costs—making them unsuitable for small institutes or training centers. 

This project proposes a low-cost alternative leveraging digital image processing and computer vision. By utilizing standard A4 paper and common image acquisition devices (ordinary scanners or mobile phone cameras), the system provides a flexible and accessible solution. We have developed a software application capable of grading custom MCQ templates, implementing algorithms that can handle both high-resolution scans and mobile-captured images.

---

## Chapter 1: Introduction

### 1. Introduction
Optical Mark Recognition (OMR) is the process of capturing human-made marks on documents, such as surveys and examination papers. Traditional OMR devices use dedicated scanners that direct light beams onto a form; the contrast in reflected light (or light transmission in transparent forms) determines whether a mark is present.

Unlike Optical Character Recognition (OCR), OMR does not require complex pattern recognition because marks are designed to be simple and consistent, significantly reducing error rates. Beyond education, OMR is used in healthcare, government voting, and data collection to streamline data entry and minimize human error.

While hardware-based OMR is fast and accurate, it is constrained by rigid requirements regarding paper size, quality, template design, and the type/color of ink used. This project aims to move automated correction into an "open-option" environment.

### 1.1 Challenges and Problems
Digital correction is often viewed as a solved problem, yet existing hardware solutions face several challenges:
1. **Accuracy in Open Environments:** Systems must be robust against rotation, noise, varying lighting conditions, and the diverse information present on an exam sheet.
2. **Execution Time:** The system must process sheets rapidly to be viable for large-scale grading.
3. **Template Flexibility:** Different institutions use different formats. A flexible system should allow custom templates without requiring expensive pre-printed forms.

### 1.2 Project Objectives
1. **Mark Detection:** Automatically identify the location of options for each question and detect which options have been marked.
2. **Evaluation:** Grade the student's answers by comparing detected marks against a stored reference key.
3. **Versatility:** Support image acquisition via both standard scanners and mobile phone cameras.

### 1.3 Workflow
The project followed these stages:
- Requirements Gathering $\rightarrow$ Analytical Study $\rightarrow$ Design Study $\rightarrow$ Environment Setup $\rightarrow$ Implementation $\rightarrow$ Testing $\rightarrow$ Expert Evaluation $\rightarrow$ Documentation.

---

## Chapter 2: Theoretical Framework and Literature Review

### 2. Automated Correction System
An automated correction system consists of several key stages:
1. **Input Stage:** Acquiring the sheet as a digital image (via scanner or camera).
2. **Processing Stage:** The core of the system. It involves identifying the sheet, correcting geometric distortions, and locating the options.
3. **Evaluation Stage:** Comparing detected marks with the reference key (provided as a text file or a reference image).
4. **Output Stage:** Generating results, calculating grades, and identifying the student.
5. **Reporting Stage:** Generating detailed reports and statistics.

### 2.1 Processing Stage Details
The processing stage is divided into three main parts:

#### 2.1.1 Sheet Identification and Geometric Correction
Images captured via cameras often suffer from shift, rotation, skew, and perspective distortion. To fix this, the system uses:
- **Digital Image Processing:** Spatial and frequency domain filters to correct rotation (aligning to 90°), perspective transforms, and smoothing filters to reduce noise.
- **AI/Computer Vision Descriptors:** Using feature descriptors like HOG, SIFT, and KAZE. This project specifically utilizes **SURF (Speed-Up Robust Features)** for mobile images.

**SIFT vs. SURF:**
- **SIFT (Scale Invariant Feature Transform):** Extracts keypoints and descriptors that are invariant to scale and rotation. However, it is computationally expensive.
- **SURF (Speed-Up Robust Features):** A faster version of SIFT that approximates the Laplacian of Gaussian using box filters and uses the Hessian matrix determinant. It is significantly faster (up to 3x) while maintaining similar performance.

#### 2.1.2 Option Localization
Options can be located either by edge-following (dynamic detection) or by providing the system with expected coordinates (static detection). This project uses a hybrid approach where the template's expected coordinates are known, which increases processing speed and reliability.

#### 2.1.3 Mark Recognition
The system iterates through each question, searching for pixels that differ from the background. By converting the image to grayscale and applying **Otsu's Binarization**, the system creates a binary inverted image (marks become white, background becomes black). A mark is detected if the number of white pixels within a specific radius of the option center exceeds a defined threshold.

---

## Chapter 3: Implementation

### 3.1 Development Methodology
The project used **Throwaway Prototyping**, an iterative process of analysis, design, and implementation to refine technical challenges before finalizing the system.

### 3.2 Analysis
**Functional Requirements:**
- Handle physical answer sheets.
- Recognize student IDs and answers.
- Calculate and store grades.
- Export reports.

**Non-Functional Requirements:**
- OS: Windows 7 SP1 or newer.
- Framework: .NET Framework 4.0+.
- Dependencies: Visual C++ 2015 Runtime (for EmguCV).

### 3.3 Design
The system is divided into three modules:
1. **Recognition Module:** Handles image acquisition, SURF matching, and mark detection.
2. **User Interface Module:** Built with C# Windows Forms.
3. **Data Processing Module:** Handles comparison, SQLite storage, and Excel export.

#### 3.3.1 Template Design
The template was designed in Photoshop for A4 paper. It features a 5-digit student ID section and 80 questions. The layout ensures equal spacing between options to simplify coordinate calculation.

#### 3.3.2 Image Acquisition
- **Scanner:** Uses the `Saraff.Twain` library to interface with scanners (e.g., HP Deskjet F2180) at 100 DPI.
- **Camera:** Supports 1MP images captured under good lighting.

#### 3.3.3 The Algorithm
- **`get_IDCentersLocations` & `get_QuesCentersLocations`:** Calculate the center points of all bubbles based on template coordinates and user-defined offsets (X, Y).
- **`getAnswers`:** Uses `getWhitePixelsInBlob` to count non-background pixels. If the count exceeds the threshold, the option is marked `True`.
- **`MatchDetect`:** For camera images, it uses SURF to find keypoints between a "Model Image" and the "Observed Image," calculating a **Homography Matrix** to warp the coordinates to the correct positions.
- **`compare`:** Compares the student's answer dictionary against the reference dictionary. An answer is marked wrong if:
    - No option is marked.
    - Multiple options are marked.
    - The marked option does not match the key.

### 3.4 Technical Stack
- **Language:** C# (.NET Framework)
- **Computer Vision:** EmguCV (OpenCV wrapper)
- **Database:** SQLite
- **Excel Export:** EPPlus
- **Scanner Interface:** Saraff.Twain
- **Design Tools:** Photoshop, Visio

### 3.5 Testing and Results
- **Scanner Tests:** Successfully recognized marks using blue ink. Handled edge cases like over-filling or under-filling bubbles.
- **Calibration:** Implemented dynamic X/Y offset and radius adjustments in the settings menu to handle printing/scanning shifts.
- **Camera Tests:** Successfully implemented SURF matching. Identified that colored markers are more effective for keypoint extraction than grayscale logos.

---

## Chapter 4: Conclusion and Future Work
The use of computer vision for OMR is a promising, low-cost evolution of automated grading. With the rise of powerful mobile hardware, these systems can become fully portable.

**Future Goals:**
- Implement Deep Learning for more robust mark detection.
- Improve rotation and skew correction for scanner images.
- Develop a native Android/iOS application for real-time grading.
