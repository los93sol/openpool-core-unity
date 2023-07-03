import cv2
import depthai

# Create a DepthAI pipeline
pipeline = depthai.Pipeline()

# Define the sources and outputs for the pipeline
cam_rgb = pipeline.createColorCamera()
xout_rgb = pipeline.createXLinkOut()
xout_depth = pipeline.createXLinkOut()

xout_rgb.setStreamName("rgb")
xout_depth.setStreamName("depth")

cam_rgb.setResolution(depthai.ColorCameraProperties.SensorResolution.THE_1080_P)

# Link the camera and outputs
cam_rgb.preview.link(xout_rgb.input)
cam_rgb.preview.link(xout_depth.input)

# Create DepthAI device and start the pipeline
device = depthai.Device(pipeline)
device.startPipeline()

# Open a window to display the color camera feed
cv2.namedWindow("Color Camera", cv2.WINDOW_NORMAL)
cv2.namedWindow("Depth", cv2.WINDOW_NORMAL)

# Callback function for mouse click event
def mouse_callback(event, x, y, flags, param):
    if event == cv2.EVENT_LBUTTONDOWN:
        # Get the depth at the clicked point
        depth_data = param.get()
        depth_at_point = depth_data[y, x]

        print("Clicked point: ({}, {})".format(x, y))
        print("Depth at clicked point: {} mm".format(depth_at_point))

# Set the callback function for mouse events
cv2.setMouseCallback("Color Camera", mouse_callback)

# Main loop to process frames
while True:
    # Get the depth and color frames from the device
    depth_data = device.getOutputQueue("depth").get()
    color_data = device.getOutputQueue("rgb").get()

    # Convert the frames to numpy arrays
    depth_frame = depth_data.getData().reshape((300, 300)).astype(float)
    color_frame = color_data.getData().reshape((3, 1080, 1920)).transpose(1, 2, 0).astype(int)

    # Normalize the depth frame for visualization
    depth_frame /= depth_frame.max()
    depth_frame *= 255

    # Resize the depth frame to match the color camera resolution
    depth_frame = cv2.resize(depth_frame, (1920, 1080), interpolation=cv2.INTER_NEAREST)

    # Display the color frame and depth frame
    cv2.imshow("Color Camera", color_frame)
    cv2.imshow("Depth", depth_frame)

    # Pass the depth frame to the mouse callback function
    cv2.setMouseCallback("Color Camera", mouse_callback, depth_frame)

    # Check for key press
    key = cv2.waitKey(1)
    if key == ord('q'):
        break

# Release resources
cv2.destroyAllWindows()
device.close()
