import React from "react";
import loginImg from "../../login.svg";
import {apiUrl, jwtKey} from "../../App";
import {Navigate} from "react-router-dom";

export class Login extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: "",
            isSuccess: false,
            isFailure: false,
        };
    }

    componentDidMount() {
        if (localStorage.getItem(jwtKey) != null) {
            this.handleIsSuccessChange(true)
        }
    }

    handleEmailChange = event => {
        this.setState({email: event.target.value, isSuccess: false, isFailure: false})
    }

    handlePasswordChange = event => {
        this.setState({password: event.target.value, isSuccess: false, isFailure: false})
    }

    handleIsSuccessChange = isSuccess => {
        this.setState({isSuccess: isSuccess})
    }

    handleIsFailureChange = isFailure => {
        this.setState({isFailure: isFailure})
    }

    handleLogin = () => {
        let xhr = new XMLHttpRequest();
        let url = apiUrl + "/authentication/login";
        this.handleIsSuccessChange(false)
        this.handleIsFailureChange(false)

        xhr.open("POST", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onreadystatechange = () => {
            if (xhr.readyState === 4) {
                if (xhr.status >= 200 && xhr.status < 300) {
                    this.handleIsSuccessChange(true)
                    localStorage.setItem(jwtKey, xhr.responseText)
                } else {
                    this.handleIsFailureChange(true)
                }
            }
        };

        const data = JSON.stringify({"email": this.state.email, "password": this.state.password});
        xhr.send(data);
    }

    render() {
        return (
            <div className="base-container" ref={this.props.containerRef}>
                <div className="header">Login</div>
                <div className="content">
                    <div className="image">
                        <img src={loginImg}/>
                    </div>
                    <div className="form">
                        <div className="form-group">
                            <label htmlFor="username">Email</label>
                            <input type="text" name="email" placeholder="example@email.com"
                                   onChange={this.handleEmailChange}/>
                        </div>
                        <div className="form-group">
                            <label htmlFor="password">Password</label>
                            <input type="password" name="password" placeholder="Password"
                                   onChange={this.handlePasswordChange}/>
                        </div>
                    </div>
                </div>
                {this.state.isSuccess && (<Navigate to="/shops"/>)}
                {this.state.isFailure && <span style={{color: "red"}}>Failed to login</span>}
                <div className="footer">
                    <button type="button" className="btn" onClick={this.handleLogin}>
                        Login
                    </button>
                </div>
            </div>
        );
    }
}
