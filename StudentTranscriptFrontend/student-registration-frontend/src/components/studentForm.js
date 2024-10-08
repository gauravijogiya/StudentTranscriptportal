import React, { useState } from "react";
import axios from "axios";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as Yup from "yup";

const StudentForm = () => {
  const [submitStatus, setSubmitStatus] = useState(null); // To display success/error messages
//const phoneString="\^\d(?:\+1)?\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$";
  // Define the validation schema using Yup
  const validationSchema = Yup.object().shape({
    name: Yup.string()
      .required("Name is required")
      .max(100, "Name cannot exceed 100 characters"),
    email: Yup.string()
      .required("Email is required")
      .email("Email is invalid"),
    contactNumber:Yup.number()
    .positive("A phone number can't start with a minus")
    .integer("A phone number can't include a decimal point")
    .min(10)
    .required('A phone number is required'),
    //.matches(phoneString),
    transcriptYear: Yup.string()
      //.required("Last year is required")
      .matches(/^\d{4}$/, "Enter a valid year")
      .test((value)=>value<=new Date().getFullYear()),
    transcript: Yup.mixed()
      .required("Transcript is required")
      .test(
        "fileType",
        "Only PDF files are accepted",
        (value) => value && value[0] && value[0].type === "application/pdf" //||"image/jpeg"||"image/png"
      )
      .test(
        "fileSize",
        "File size should be less than 10MB",
        (value) => value && value[0] && value[0].size <= 10458760
      ),
  });

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    reset,
  } = useForm({
    resolver: yupResolver(validationSchema),
  });

  // Function to generate JWT Token (for testing purposes)
  const generateTestToken = () => {
        return "your_jwt_token_here"; // Replace with a valid JWT
  };

  const onSubmit = async (data) => {
    setSubmitStatus(null); // Reset status
    const formData = new FormData();
    formData.append("Name", data.name);
    formData.append("ContactNumber",data.contactNumber);
    formData.append("Email", data.email);
    formData.append("TranscriptYear", data.transcriptYear);
    formData.append("Transcript", data.transcript[0]);


    try {
      const token = generateTestToken(); // Obtain JWT Token
      for (var pair of formData.entries()) {
        console.log(pair[0]+ ', ' + pair[1]); 
        console.log("token"+token);
      }
      const response = await axios.post(
        "https://localhost:7166/api/Students", 
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
            Authorization: `Bearer ${token}`, // Attach JWT token
          },
        }
      );
      //console.log(response);
      let {name,email}=response.data;
      setSubmitStatus({ success: true, message: "Thank you "+name+" for submitting your Transcript ." });
      reset(); // Reset form fields
    } catch (error) {
      let message = "An error occurred. Please try again.";
      if (error.response && error.response.data) {
        message = error.response.data;
      }
      setSubmitStatus({ success: false, message });
    }
  };

  return (
    <div className="container mt-5">
      <h2>Student Registration Form</h2>
      <form onSubmit={handleSubmit(onSubmit)} noValidate>
        {/* Name Field */}
        <div className="form-group mb-3">
          <label htmlFor="name">Name</label>
          <input
            id="name"
            name="name"
            type="text"
            {...register("name")}
            className={`form-control ${errors.name ? "is-invalid" : ""}`}
          />
          <div className="invalid-feedback">{errors.name?.message}</div>
        </div>
        {/*contactNumber Field */}
        <div className="form-group mb-3">
          <label htmlFor="contactNumber">ContactNumber</label>
          <input
            id="contactNumber"
            name="contactNumber"
            type="text"
            {...register("contactNumber")}
            className={`form-control ${errors.contactNumber ? "is-invalid" : ""}`}
          />
          <div className="invalid-feedback">{errors.contactNumber?.message}</div>
        </div>

        {/* Email Field */}
        <div className="form-group mb-3">
          <label htmlFor="email">Email</label>
          <input
            id="email"
            name="email"
            type="email"
            {...register("email")}
            className={`form-control ${errors.email ? "is-invalid" : ""}`}
          />
          <div className="invalid-feedback">{errors.email?.message}</div>
        </div>

        {/* Last Year Field */}
        <div className="form-group mb-3">
          <label htmlFor="transcriptYear"> Year of Academic Transcript</label>
          <input
            id="transcriptYear"
            name="transcriptYear"
            type="text"
            {...register("transcriptYear")}
            className={`form-control ${errors.transcriptYear ? "is-invalid" : ""}`}
            placeholder="e.g., 2023"
          />
          <div className="invalid-feedback">{errors.transcriptYear?.message}</div>
        </div>

        {/* Transcript Upload Field */}
        <div className="form-group mb-3">
          <label htmlFor="transcript">Upload Transcript (PDF)</label>
          <input
            id="transcript"
            name="transcript"
            type="file"
            accept="application/pdf"
            {...register("transcript")}
            className={`form-control ${errors.transcript ? "is-invalid" : ""}`}
          />
          <div className="invalid-feedback">{errors.transcript?.message}</div>
        </div>

        {/* Submit Button */}
        <button
          type="submit"
          className="btn btn-primary"
          disabled={isSubmitting}
        >
          {isSubmitting ? "Submitting..." : "Register"}
        </button>

        {/* Submission Status */}
        {submitStatus && (
          <div
            className={`mt-3 alert ${
              submitStatus.success ? "alert-success" : "alert-danger"
            }`}
            role="alert">
            {submitStatus.message}
          </div>
        )}
      </form>
    </div>
  );
};

export default StudentForm;
