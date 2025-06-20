This project provides a backend system built with **.NET** that enables users to:

- Search for plant information by name.
- Upload an image of a plant to identify its name.
- Upload an image of a plant's leaf to detect potential diseases.

It integrates external APIs for image recognition and disease detection, and returns detailed information about the plant or its health status.

---

## Features

1. **Plant Search by Name**  
   Users can search for plants by entering a name. The system will return:
   - Two images of the plant.
   - Detailed botanical and care-related information.

2. **Plant Identification from Image**  
   Users can upload a picture of a plant. The backend will:
   - Send the image to an external **Plant Identification API**.
   - Get the plant's name.
   - Search the internal database for plant details.
   - Return full plant data to the user.

3. **Plant Disease Detection**  
   Users can upload a leaf image to check for diseases. The backend:
   - Sends the image to an external **Plant Disease API**.
   - Receives a response indicating:
     - Whether the plant is healthy or infected.
     - Disease name and details if present.
   - Returns this diagnosis to the user.

---

##  Technologies Used

- **.NET Core / .NET 8**
- **C#**
- **External APIs for image recognition & disease detection**
- **RESTful API design**
- **Entity Framework / SQL**
