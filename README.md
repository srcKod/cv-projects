# Computer Vision Projects: Mobile Recognition & OMR Automation

Two computer vision projects completed during a B.S. program in Information Engineering, Syrian Virtual University (SVU). They implement classic image processing and feature-matching pipelines for real-time object detection on mobile and document analysis on desktop.

**Academic Context:**
Both projects were developed as supervised university projects during 2015–2017. At the time, classical computer vision (Haar cascades, SIFT/SURF descriptors) was the standard approach to these problem domains—deep learning-based detectors were not yet practical for this toolchain, hardware, and target platforms. The techniques used are therefore those that were current in the field when the work was done.

- **PR1 (2015–2016):** Mid-program project — smartphone money recognition for accessibility.
- **PR2 (2016–2017):** Final project — digital OMR correction system for MCQ examinations.

Each project is a self-contained case study (problem → approach → implementation → results) and follows the same layout convention so it can be read standalone: `README.md` for build/run, `src/` for code, `docs/` for the full report, demo, and test assets.

## Technical Implementations

### 1. Smart Phone Money Recognition (PR1)
**Objective:** Real-time currency denomination recognition for the visually impaired.

*   **Platform:** Android (Java/C++ via JNI/NDK).
*   **Core Algorithm:** **Haar Cascade Classifiers** trained with **AdaBoost**.
*   **Implementation Details:**
    *   Developed a native C++ processing layer to handle camera frames for real-time performance.
    *   Trained a custom classifier to detect a specific geometric feature (octagonal element) on banknotes.
    *   Integrated Android Text-to-Speech (TTS) for audio-based value announcement.
*   **Engineering Challenge:** Implementing a cross-language bridge (JNI) to run OpenCV detection on resource-constrained mobile hardware, and training the classifier under limited positive/negative sample availability.
*   **Result:** Real-time detection + TTS announcement of the 500 SYP note on a physical device (single-denomination proof-of-concept).

### 2. Digital OMR Correction System (PR2)
**Objective:** Automated grading of MCQ examination sheets using standard image acquisition (scanner or mobile camera).

*   **Platform:** Windows (.NET/C#).
*   **Core Algorithm:** **SURF (Speed-Up Robust Features)** matching and **Homography**.
*   **Implementation Details:**
    *   **Image Registration:** SURF keypoint extraction and matching to align captured images (scanned or photographed) to a reference template.
    *   **Geometric Correction:** Homography matrix calculation to warp perspective-distorted images into the template's normalized coordinate system.
    *   **Mark Detection:** **Otsu's Binarization** for adaptive thresholding to detect filled bubbles regardless of lighting or ink intensity.
    *   **Data Management:** Embedded SQLite storage for student records and grades; EPPlus export to Excel.
*   **Engineering Challenge:** Robustness against rotation, skew, and perspective distortion in non-controlled capture environments, plus calibration for physical print/scan shifts.
*   **Result:** Graded 5 scanned + 7 camera-captured 80-question sheets against a known answer key; handles blue ink, over/under-filled bubbles, and print/scan drift. [End-to-end demo](PR2/docs/assets/demo.mp4).

Full technical documentation for each project (algorithm details, design decisions, testing) is in the `docs/` folder of each project.

---

## Technical Comparison

| Feature | PR1: Money Recognition | PR2: OMR System |
| :--- | :--- | :--- |
| **Academic Year** | 2015–2016 | 2016–2017 |
| **Platform** | Android (Mobile) | Windows (Desktop) |
| **Languages** | Java + C++ (JNI) | C# (.NET) |
| **CV Library** | OpenCV Android SDK | EmguCV |
| **Core Algorithm** | Haar Cascades / AdaBoost | SURF / Homography |
| **Primary Goal** | Real-time Object Detection | Image Registration & Analysis |
| **Data Storage** | Local State | SQLite |

---

## Repository Structure

```text
├── PR1/
│   ├── README.md       # Build instructions & dependencies
│   ├── src/            # Android Studio source code
│   ├── docs/           # Technical report, presentation, assets
│   └── output/         # Final APK (local, not tracked)
├── PR2/
│   ├── README.md       # Build instructions & dependencies
│   ├── src/            # C# source code
│   ├── docs/           # Technical report, presentation, templates & test images
│   └── output/         # Final executable (local, not tracked)
└── README.md
```

## License
Open-sourced for educational purposes.
