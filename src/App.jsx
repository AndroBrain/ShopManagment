import React from "react";
import "./App.scss";
import {BrowserRouter, Route, Routes,} from "react-router-dom";
import Shops from "./components/shop/shops";
import AddShop from "./components/shop/addshop";
import UpdateShop from "./components/shop/updateshop";
import {LoginRegister} from "./components/login/loginregister";
import Products from "./components/product/products";
import AddProduct from "./components/product/addproduct";

class App extends React.Component {

    render() {
        return (
            <>
                <BrowserRouter>
                    <Routes>
                        <Route exact path="/" element={<LoginRegister/>}/>

                        <Route path="/shops" element={<Shops/>}/>
                        <Route path="/shops/add" element={<AddShop/>}/>
                        <Route path="/shops/edit/:id&:name&:type" element={<UpdateShop/>}/>
                        <Route path="/products/:id&:name" element={<Products/>}/>
                        <Route path="/products/add/:id" element={<AddProduct/>}/>
                    </Routes>
                </BrowserRouter>
            </>
        );
    }
}

export const apiUrl = "https://localhost:7215"
export const jwtKey = "JWT_KEY"

export default App;
