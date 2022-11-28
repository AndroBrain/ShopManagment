import React from "react";
import loginImg from "../../login.svg";
import {apiUrl} from "../../App";

export class Register extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: "",
            email: "",
            password: "",
            isSuccess: false,
            isFailure: false,
        };
    }

    handleNameChange = event => {
        this.setState({name: event.target.value, isSuccess: false, isFailure: false,})
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

    handleRegister = () => {
        this.handleIsSuccessChange(false)
        this.handleIsFailureChange(false)
        let xhr = new XMLHttpRequest();
        let url = apiUrl + "/authentication/register";

        xhr.open("POST", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onreadystatechange = () => {
            if (xhr.readyState === 4) {
                if (xhr.status >= 200 && xhr.status < 300) {
                    this.handleIsSuccessChange(true)
                } else {
                    this.handleIsFailureChange(true)
                }
            }
        };

        const data = JSON.stringify({
            "name": this.state.name,
            "email": this.state.email,
            "password": this.state.password
        });
        xhr.send(data);
    }

    render() {
        return (
            <div className="base-container" ref={this.props.containerRef}>
                <div className="header">Register</div>
                <div className="content">
                    <div className="image">
                        <img src={loginImg}/>
                    </div>
                    <div className="form">
                        <div className="form-group">
                            <label htmlFor="name">Name</label>
                            <input type="text" name="name" placeholder="Name" onChange={this.handleNameChange}/>
                        </div>
                        <div className="form-group">
                            <label htmlFor="email">Email</label>
                            <input type="email" name="email" placeholder="example@email.com"
                                   onChange={this.handleEmailChange}/>
                        </div>
                        <div className="form-group">
                            <label htmlFor="password">Password</label>
                            <input type="password" name="password" placeholder="Password"
                                   onChange={this.handlePasswordChange}/>
                        </div>
                    </div>
                </div>
                {this.state.isSuccess && <span style={{color: "green"}}>Successfully registered new User</span>}
                {this.state.isFailure && <span style={{color: "red"}}>Failed to register new User</span>}
                <div className="footer">
                    <button type="button" className="btn" onClick={this.handleRegister}>
                        Register
                    </button>
                </div>
            </div>
        );
    }
}
