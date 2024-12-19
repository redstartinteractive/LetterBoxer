# Letter Boxer

* Preserve your 2D game's aspect ratio
* Works on mobile devices
* Works with Unity UI canvas
* Modifies a Camera's Rect and does not rely on any UI framework to render

### Set up the Letter Boxer
1. Attach the `Letter Boxer` script to your game camera

### Set up the UI to scale with the letterboxed camera

#### For `Screen Space - Camera` canvas
1. Select your Canvas and set the `Render Mode` to `Screen Space - Camera`
2. Set the `Render Camera` to the camera that has the `Letter Boxer` script attached

#### For `Screen Space - Overlay` canvas
1. Add a `Letter Boxer Overlay` component to your camera
2. Set the `Target` to the RectTransform that should be scaled
