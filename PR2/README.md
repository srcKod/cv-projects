# PR2: Digital OMR Correction System (2016–2017)

Final university project. An automated correction system for Multiple Choice Question (MCQ) examination sheets based on Optical Mark Recognition (OMR). It replaces expensive dedicated OMR scanners with standard image acquisition: sheets are read from a desktop scanner or captured with a mobile phone camera, then processed, graded, and exported.

## How It Works

**Scanner mode:**
1. The sheet is scanned at 100 DPI via the TWAIN interface (`Saraff.Twain`).
2. The image is converted to grayscale and binarized with **Otsu's thresholding** (adaptive, inverted — marks become white).
3. For each question, the number of white pixels inside a bubble radius is compared against a threshold to decide whether the option is marked.

**Camera mode:**
1. A photographed student sheet is matched against a clean reference template using **SURF** keypoint detection and matching.
2. A **Homography matrix** is computed from the matched points and used to warp the photographed sheet into the template's coordinate system, correcting rotation, skew, and perspective.
3. Mark detection then proceeds as in scanner mode.

**Grading:**
- The student's detected answer set is compared against the answer key (`AnswerKey.txt`). An answer is marked wrong if it is empty, has multiple marks, or does not match the key.
- Results are stored in an embedded **SQLite** database and exportable to Excel (`EPPlus`).

## Results

Validated against a known **80-question answer key** using the sample set in `docs/assets/test-images/`:

- **5 scanned answer sheets** (flatbed scanner, 100 DPI) — `scanned-images/`
- **7 mobile-captured sheets** (phone camera) — `mobile-images/`

What the test set confirmed:

- **Blue-ink marks** are recognized, and **over-filled / under-filled bubbles** are still classified by the white-pixel fill ratio, so partial fills do not flip an answer.
- **Fiducial contrast drives SURF reliability:** colored registration markers matched far more consistently than grayscale logos, so camera-mode templates use colored markers.
- **Print/scan drift** across different printers and scanners is absorbed by the X/Y offset and bubble-radius calibration in the settings dialog.

A short end-to-end run (scan → detect → grade → export) is in [`docs/assets/demo.mp4`](docs/assets/demo.mp4).

## Source Layout

```text
PR2.OMR/
├── OMR.cs            # Core CV pipeline: Otsu detection, SURF matching, homography
├── DataProcess.cs    # Answer comparison, grading, SQLite access
├── ExportToExcel.cs  # Excel report generation (EPPlus)
├── Main.cs           # Entry point / main window
├── Students/Courses/Results/...  # WinForms UI modules
├── DataBase/         # Embedded SQLite database + dataset
└── Licenses/         # Third-party license texts
```

## Dependencies

**Build environment (as originally used):**

| Component | Version |
| :--- | :--- |
| IDE | Visual Studio 2015 |
| Framework | .NET Framework 4.5 |
| EmguCV (emgucv-windesktop) | 3.2.0.2682 |
| System.Data.SQLite.Core | 1.0.105.2 (NuGet) |
| EPPlus | 4.1.0 (NuGet) |
| Saraff.Twain | 1.0.26.605 (NuGet) |
| EntityFramework | 6.0.0 (NuGet) |

**Notes on the build setup:**

- NuGet dependencies are listed in `packages.config` and restore automatically in Visual Studio (the `packages/` folder is intentionally not committed).
- EmguCV is **not** a NuGet dependency here: the project references four DLLs (`Emgu.CV.World`, `Emgu.CV.UI`, `Emgu.CV.UI.GL`, `Emgu.CV.DebuggerVisualizers.VS2015`) from a local `emgucv-windesktop 3.2.0.2682` installation. Download that build from Emgu's site and either point the reference HintPaths at its `bin/` folder or copy the DLLs next to the project.
- Runtime requires the **Visual C++ 2015 Redistributable** (x86 or x64) for EmguCV's native OpenCV binaries.
- Scanner mode additionally requires a TWAIN-compatible scanner; the app also accepts manually loaded images, so it runs without one.

## Running

1. Restore NuGet packages (automatic in Visual Studio).
2. Make the four EmguCV DLLs resolvable (see notes above).
3. Build for x86 or x64 and run the compiled app.
4. Sample templates and test images (scanned sheets, camera captures, answer key) are in `docs/assets/`.

## Documentation

- `docs/report.md` — full technical report
- `docs/slides.md` — presentation
- `docs/assets/` — template source (Photoshop), scanned and mobile test images, answer key
- `output/` — final executable (local, not tracked)
