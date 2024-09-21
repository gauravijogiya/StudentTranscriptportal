import React, { useState } from "react";
import axios from "axios";
import PhoneInput from 'react-phone-input-2'
import 'react-phone-input-2/lib/style.css'

const StudentRegistrationForm = () => {
    const [StudentId, setStudentId] = useState(1);
  const [name, setName] = useState("");
  const [contactNumber, setcontactNumber] = useState("");
  const [email, setEmail] = useState("");
  const [transcriptTitle, setTranscripttitle] = useState("");
  const [transcript, setTranscript] = useState(null);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append("Name", name);
    formData.append("ContactNumber", contactNumber);
    formData.append("Email", email);
    formData.append("TranscriptTitle", transcriptTitle);
    formData.append("Transcript", transcript);

    try {
        for (var pair of formData.entries()) {
            console.log(pair[0]+ ', ' + pair[1]); 
        }
      const response = await axios.post("https://localhost:7166/api/Students", formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });

      alert("Registration successful!");
    } catch (error) {
      console.error("There was an error!", error);
    }
  };

  return (
    
    <form onSubmit={handleSubmit} >   
   
<div className="row mb-3 g-3">    
    <label for="formGroupExampleInput" className="col-sm-2 col-form-label">Name</label>
    <div className="col-sm-10">
    <input type="text" className="form-ontrol"  id="formGroupExampleInput" placeholder="Enter Name" value={name} onChange={(e) => setName(e.target.value)} required />
    </div>  
</div>

<div className="row mb-3">    
    <label for="formGroupExampleInput" className="col-sm-2 col-form-label">ContactNumber</label>
    <div className="col-sm-4"></div>
    <div className="col-sm-10">
    <PhoneInput
  country={'us'}
  value={this.state.contactNumber} inputProps={{
    name: 'contactNumber',
    required: true,
    autoFocus: true
  }}
  onChange={contactNumber => this.setState({ contactNumber })}
/>
    {/* <input type="text" className="form-ontrol" value={contactNumber} onChange={(e) => setcontactNumber(e.target.value)} required /> */}
    </div>  
</div>
  
      <div className="row mb-3">
        <label for="Email" className="col-sm-2 col-form-label">Email</label>
        <div className="col-sm-10">
        <input type="email" className="form-ontrol" value={email} onChange={(e) => setEmail(e.target.value)} required />
        </div>  
      </div>
      <div className="row mb-3">
        <label for="TrascriptTitle" className="col-sm-2 col-form-label">TranscriptTitle</label>
        <div className="col-sm-10">
        <input type="text" value={transcriptTitle} onChange={(e) => setTranscripttitle(e.target.value)} required />
        </div>
      </div>
      <div className="row mb-3">
        <label for="Trascript" className="col-sm-2 col-form-label">Transcript</label>
        <div className="col-sm-10">
        <input type="file" onChange={(e) => setTranscript(e.target.files[0])} required />
        </div>
      </div>
      <button type="submit"  className="btn btn-primary">Register</button>
    </form>
   
   
  );
};

export default StudentRegistrationForm;
