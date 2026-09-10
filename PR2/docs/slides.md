# Presentation: OMR Digital Correction System

## Slide 1: Title
**Project:** Digital Correction System (OMR)
**Institution:** Syrian Virtual University (SVU) - Information Engineering Program

---

## Slide 2: Presentation Outline
1. Introduction & Motivation
2. Project Objectives
3. Technical Stack
4. Use Case Diagrams
5. Database Design
6. Development Environment Setup
7. User Environment Requirements
8. Implementation Details
9. Testing & Results
10. Conclusion & Future Work
11. References

---

## Slide 3: Introduction & Motivation
**What is OMR?**
Optical Mark Recognition (OMR) is the process of capturing human-made marks on documents (surveys, exams). It is widely used in healthcare, voting, and education to reduce data entry errors.

**The Problem:**
Traditional OMR systems rely on:
- Expensive, specialized hardware scanners.
- Rigid, proprietary paper templates.
- Strict requirements for paper quality and ink color.

**The Opportunity:**
Modern Computer Vision and Image Processing allow us to create a flexible, low-cost system using standard A4 paper and ordinary cameras/scanners.

---

## Slide 4: Research Challenges
1. **Accuracy:** Robustness against rotation, noise, varying lighting, and open-environment image capture.
2. **Performance:** Minimizing execution time for rapid grading.
3. **Flexibility:** Supporting custom templates that vary between institutions.

---

## Slide 5: Project Objectives
**Goal:** Design an automated correction system as a low-cost alternative to manual grading and expensive OMR hardware.

**Key Functions:**
- Detect the location of options for every question.
- Identify marked bubbles.
- Compare student marks against a reference key.
- Store, display, and export results.

---

## Slide 6: Technical Stack
- **Language:** C# (.NET Framework)
- **Platform:** Windows
- **Computer Vision:** EmguCV (OpenCV Wrapper)
- **Database:** SQLite (Embedded)
- **IDE:** Visual Studio 2015
- **Design:** Photoshop (Templates), Microsoft Visio (Diagrams)

---

## Slide 7: Use Case Diagrams
- **Scanner Workflow:** Image Acquisition $\rightarrow$ Processing $\rightarrow$ Grading $\rightarrow$ Results.
- **Camera Workflow:** Reference Image $\rightarrow$ Student Image $\rightarrow$ SURF Matching $\rightarrow$ Grading $\rightarrow$ Results.

---

## Slide 8: Database Design
- **ERD:** Designed to manage Students, Courses, and Results.
- **Storage:** SQLite was chosen to ensure the application is portable and requires no external database server installation.

---

## Slide 9: Development Environment Setup
- **Framework:** .NET Framework 4.5.
- **Architecture:** Target x64 or x86.
- **Dependencies:** 
    - EmguCV 3.2 (DLLs added to project folder).
    - SQLite Integration for Visual Studio.
- **NuGet Packages:** `System.Data.SQLite`, `Saraff.Twain`, `EPPlus`.

---

## Slide 10: User Environment Requirements
- **OS:** Windows 7 SP1 or newer.
- **Runtime:** .NET 4.5 Framework.
- **C++ Redistributable:** Visual C++ 2015 (x86 or x64).
- **Portability:** The app can be run from a USB drive (Portable).

---

## Slide 11: Implementation
**Core Architecture:**
- **`OMR` Class:** Handles image processing, mark detection, and SURF matching.
- **`DataProcess` Class:** Manages memory storage, answer comparison, and grading logic.
- **`ExportToExcel` Class:** Generates `.xlsx` reports using EPPlus.

---

## Slide 12: Testing - Scanner Mode
- **Process:** Load image $\rightarrow$ Apply Otsu Thresholding $\rightarrow$ Detect Bubbles.
- **Results:** Successfully recognized blue ink.
- **Edge Cases:** Handled multiple marks (marked as wrong) and empty answers.
- **Calibration:** Dynamic X/Y offset and radius adjustments implemented to fix printing shifts.

---

## Slide 13: Testing - Camera Mode
- **Process:** Use SURF to match a student's photo to a reference template.
- **Homography:** Calculates a transformation matrix to align the photo with the template coordinates.
- **Findings:** Colored markers provide better keypoint extraction than grayscale logos.

---

## Slide 14: Conclusion & Future Work
**Conclusion:**
Computer Vision-based OMR is a viable, low-cost evolution of automated grading, making the technology accessible to small centers.

**Future Vision:**
- **Deep Learning:** Use CNNs for more accurate mark detection.
- **Mobile App:** Develop a native Android/iOS app for real-time grading.
- **Robustness:** Improve automatic skew and rotation correction.

---

## Slide 15: References
*(List of academic and technical references used in the project)*
