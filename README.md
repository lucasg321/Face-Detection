# Face-Detection
Made using EmguCV (an OpenCV wrapper for .NET) and C#

<img src="https://lucasgigliozzi.com/wp-content/uploads/2019/11/facedetection-768x434.png" width="620" height="400" />

### Description: 
The Face Detection application receives video feed from a webcam and uses OpenCV to detect faces in the frames.

### Languages/Libraries/Frameworks: 
C#, .NET, OpenCV/EmguCV

### Explanation: 
Inspired by a video I watched, wherein a program used webcam input to recognize faces and obtain the position of a person, then used the coordinates to aim a laser at the persons face. My goal was to recreate this project using the same software, which is EmguCV (an OpenCV wrapper for .NET) and C# — without looking at the source code, of course. This project is a work in progress. Using the webcam and storing frames in an appropriate data type, as well as the image analysis part of the program using OpenCV is complete. The application can start to take webcam input and be analyzed via one of two options for facial recognition. When “Aim” is clicked the x and y coordinates of approximately the middle of the face are printed to a text box. Even the Arduino code whereby the Arduino will receive x and y coordinates parse them and send them to servo motors has been made. All that is left is to buy the required motors, wires, and power supplies, and then assemble the hardware.

### How it works: 
The program takes input from a webcam and stores it as a Mat data type so OpenCV can be used to analyze the images. It continually snaps a frame from the webcam and stores it so it looks like a video feed. Currently, it is set at 500 milliseconds, however, this can be sped up or slowed down. After snapping a new frame the imageCapture function is called, which checks to see if the “Start Facial Detection Haar” or the “Start Facial Detection LBP” buttons were clicked. If one or both of them have been clicked, then the frame will be sent to a part of the code responsible for analyzing the image. Saving a frame as a Mat data type, calling the imageCapture function, and repeating this every 500 milliseconds is shown in the code below. 

![codeimage](https://lucasgigliozzi.com/wp-content/uploads/2019/11/imagecapfacedetect-768x242.png)

When the images are sent to the imageCapture function, they are then analyzed with OpenCV’s Haarcascade or LBP functions (depending on the users choice) to detect faces and eyes. A rectangular box is drawn around any faces or eyes that are detected. The code for this is shown below. First I declare a path to OpenCV’s pre-trained haar cascade data sets. Next, I create a CascadeClassifier which is an object from the OpenCV library, which uses the previously declared OpenCV data sets via cascading to find like objects in the captured frames. For this case, the data set was trained for face detection, so it will be looking for a face (more specifically, it is looking for certain features of a face, such as darker eye regions, a brighter nose region etc. based on the Viola-Jones method). Once a like object is found, it is stored as a rectangle type from System.Drawing. A rectangle is used due to the nature of the cascading method of image recognition, which uses a sliding window to match features in the window to classifiers from the data set. When enough classifiers are found in the window, then a face is detected and a rectangle of the position and magnitude of the face is returned and stored in the rectangle array. Finally, a red rectangle of the same magnitude and position is drawn onto the image for a face, and a green rectangle is used for eyes.

![codeimage2](https://lucasgigliozzi.com/wp-content/uploads/2019/11/recfacedetect-768x245.png)

