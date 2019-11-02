# Face-Detection
Made using EmguCV (an OpenCV wrapper for .NET) and C#

This program currently takes input from a webcam and stores it as an Image type that OpenCV can work with. Once the start face detection button is clicked the images will be are sent through OpenCV's Haarcascade functions to detect faces and eyes. A rectangular box is drawn around any faces or eyes that are dtected in the image and displayed on a seperate image output window. The "Aim" button can then be clicked to write the X and Y coordinates of approximately the middle of the face that is detected to a textbox and to a serial output. In the future, this serial output will be hooked up to a microcontroller which will receive the coordinates, and then control external motors to aim at the detected face.
