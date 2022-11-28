import React, {useState} from "react";
import {apiUrl, jwtKey} from "../../App";
import {useNavigate, useParams} from "react-router-dom";

function AddProduct() {
    const [productName, setProductName] = useState("")
    const [price, setPrice] = useState(0.0)
    const [isLoading, setIsLoading] = useState(false)
    const [isFailure, setIsFailure] = useState(false)

    const params = useParams()
    const navigate = useNavigate()

    const addProductToShop = (productId) => {
        setIsLoading(true)
        let xhr = new XMLHttpRequest();
        let url = apiUrl + "/Shop/AddProduct"

        xhr.open("Post", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.setRequestHeader("Authorization", 'Bearer ' + localStorage.getItem(jwtKey))
        xhr.onreadystatechange = () => {
            if (xhr.readyState === 4) {
                setIsLoading(false)
                if (xhr.status >= 200 && xhr.status < 300) {
                    window.history.back()
                } else {
                    setIsFailure(true)
                    console.log(xhr.status + '\n' + xhr.responseText)
                    if (xhr.status === 401) {
                        localStorage.removeItem(jwtKey)
                        navigate('/')
                    }
                }
            }
        };
        const data = JSON.stringify({shopId: params.id, productId: productId});
        xhr.send(data);
    }

    const createProduct = (shop) => {
        setIsLoading(true)
        let xhr = new XMLHttpRequest();
        let url = apiUrl + "/Product/Create"

        xhr.open("Post", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.setRequestHeader("Authorization", 'Bearer ' + localStorage.getItem(jwtKey))
        xhr.onreadystatechange = () => {
            if (xhr.readyState === 4) {
                setIsLoading(false)
                if (xhr.status >= 200 && xhr.status < 300) {
                    addProductToShop(JSON.parse(xhr.responseText))
                } else {
                    setIsFailure(true)
                    console.log(xhr.status + '\n' + xhr.responseText)
                    if (xhr.status === 401) {
                        localStorage.removeItem(jwtKey)
                        navigate('/')
                    }
                }
            }
        };
        const data = JSON.stringify({name: shop, price: price, shopIds: [params.id]});
        xhr.send(data);
    }

    return (
        <div className="shop-controls-group">
            <label htmlFor="username">Name</label>
            <input type="text" name="text" placeholder="Product Name"
                   onChange={e => setProductName(e.target.value)}/>
            <label htmlFor="username">Price</label>
            <input type="number" name="text" placeholder="Price"
                   onChange={e => setPrice(parseFloat(e.target.value))}/>
            {isLoading && <span>Loading...</span>}
            {isFailure && <span style={{color: "red"}}>Error</span>}
            <button type="button" className="optionsButton" onClick={() => createProduct(productName)}>Add</button>
        </div>
    )
}

export default AddProduct
