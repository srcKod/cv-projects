# PR1: Smart Phone Money Recognition (2015–2016)

Mid-program university project. Real-time banknote denomination recognition on Android, built as an accessibility tool for the visually impaired: the app detects a banknote feature through the camera and announces the recognized value via Text-to-Speech.

## How It Works

1. Camera frames are captured in `onCameraFrame()` (RGBA + grayscale).
2. The Java layer passes each frame to native C++ via JNI.
3. A **Cascade Classifier** (Haar-like features, AdaBoost) runs `detectMultiScale()` on an image pyramid to locate the trained target feature (an octagonal element on the 500 SYP note).
4. Detected regions are drawn as bounding boxes; the recognized value is spoken using Android TTS.

## Results

Tested on a **physical Android device** (the emulator of this era has no usable camera feed):

- Presenting a **500 SYP** note to the camera locates the trained **octagonal feature** in real time and draws a green bounding box around it.
- The recognized value is announced through **Text-to-Speech**.
- The detection rate improves as the in-app **Box Size** (minimum detection size) is tuned to the note's distance from the camera.

**Scope:** a working **proof-of-concept for a single denomination** (500 SYP), not a general banknote classifier. The report lists CNN-based recognition and self-learning new denominations as the intended upgrade path.

## Source Layout

```text
app/src/main/
├── java/pr1/svu/com/money/
│   ├── Money.java                 # Main activity, camera feed, TTS output
│   └── DetectionBasedTracker.java # JNI bridge to native detection
├── jni/
│   ├── Detection.cpp / .h         # Native C++: CascadeDetectorAdapter, detectMultiScale
├── res/                           # Layouts, strings, TTS assets
openCVLibrary310/                  # Vendored OpenCV 3.1.0 Java bindings (third-party, not author code)
```

## Dependencies

**Build environment (as originally used):**

| Component | Version |
| :--- | :--- |
| Android Studio | 2.1 (uses the `com.android.model` experimental Gradle NDK plugin) |
| JDK | 8 |
| Android SDK | compileSdk 23, build-tools 23.0.3 |
| Android NDK | clang toolchain, `gnustl_static` STL |
| OpenCV Android SDK | 3.1.0 (OpenCV `3.1.0` / `opencv_java3.so` per ABI) |

**Notes on the build setup:**

- `openCVLibrary310/` is the **vendored OpenCV 3.1.0 Java bindings** — a third-party library committed so the project builds without network access. It is not author code. The application-specific work is in `app/`: `Money.java`, `DetectionBasedTracker.java`, and the native `jni/` sources (~600 lines); the rest of the tree is the OpenCV module plus standard Gradle/Android scaffolding.
- The project was built with the **experimental NDK Gradle plugin** (`gradle-experimental 0.7.0`). Rebuilding with a modern Android Studio version requires migrating `app/build.gradle` from the `com.android.model` DSL to the standard `com.android.application` + `externalNativeBuild` (CMake/ndk-build) DSL. The C++ sources in `jni/` are unchanged.
- Two paths are hardcoded and must be adjusted for your machine:
    - `C:/OpenCV-android-sdk/sdk/native/jni/include` — OpenCV include dir in the NDK `cppFlags` (in `app/build.gradle`).
    - Per-flavor `jniLibs/<abi>/libopencv_java3.so` paths appended in `ldLibs` (in `app/build.gradle`).
- Gradle repositories are `jcenter()` (read-only today) + Maven Central; add `google()` if a dependency is missing.

## Running

1. Fix the two hardcoded paths above.
2. Sync Gradle, select an ABI flavor (`arm` / `armv7` / `x86` / `mips`).
3. Run on a **physical device with a camera** — the Android emulator (AVD) of this era does not provide a usable camera feed.
4. Use the in-app *Box Size* setting to tune the minimum detection size for the note's distance from the camera.

## Documentation

- `docs/report.md` — full technical report
- `docs/slides.md` — presentation
- `output/` — final APK (local, not tracked)
