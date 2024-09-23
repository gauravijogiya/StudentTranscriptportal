import { useForm } from "react-hook-form";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import axios from "axios";
import { useNavigate } from 'react-router-dom';

const schema = Yup.object().shape({
    userid: Yup.string().required("UserId is required"),
    password: Yup.string().required("Password is required"),
});

const Login = () => {
    const { register, handleSubmit, formState: { errors } } = useForm({
        resolver: yupResolver(schema),
    });
    const navigate = useNavigate();
    const onSubmit = async (data) => {
        try {
            const response = await axios.post('https://localhost:7166/api/Users/Login', data);
            // Store token in local storage or cookies
            console.log(response.data);
            localStorage.setItem('token', response.data.token);
            navigate('/studentForm');
        } catch (error) {
            console.error("Login failed:", error);
        }
    };

    return (
        <div className="container mt-5">
      <h2>Please Sign In</h2>
        <form onSubmit={handleSubmit(onSubmit)} noValidate>
            <div className="form-group mb-3">
            <label htmlFor="UserID">UserID</label>
            <input type="text" className={`form-control ${errors.userid ? "is-invalid" : ""}`} {...register("userid")} placeholder="UserId" />
            <div className="invalid-feedback">{errors.userid?.message}</div>
            </div>
            <div className="form-group mb-3">
            <label htmlFor="Password">Password</label>
            <input type="password" className={`form-control ${errors.password ? "is-invalid" : ""}`} {...register("password")} placeholder="Password" />
            <div className="invalid-feedback">{errors.password?.message}</div>

            </div>
            <button  className="btn btn-primary" type="submit">Login</button>
        </form>
        </div>
    );
};

export default Login;
