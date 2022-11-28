import React, {useState} from "react";
import "./shopstyle.scss"
import {apiUrl, jwtKey} from "../../App";
import {useNavigate, useParams} from "react-router-dom";

function UpdateShop() {
    let params = useParams()

    const [shopName, setShopName] = useState(params.name)
    const [shopType, setShopType] = useState(params.type)
    const [isLoading, setIsLoading] = useState(false)
    const [isFailure, setIsFailure] = useState(false)

    const navigate = useNavigate()

    const updateShop = () => {
        setIsLoading(true)
        let xhr = new XMLHttpRequest();
        let url = apiUrl + "/Shop/Update"

        xhr.open("Put", url, true);
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

        const data = JSON.stringify({id: params.id, name: shopName, type: {name: shopType}},);
        xhr.send(data);
    }

    return (
        <div className="shop-controls-group">
            <label htmlFor="username">Shop Name</label>
            <input type="text" name="text" placeholder="Shop Name" value={shopName}
                   onChange={e => setShopName(e.target.value)}/>
            <label htmlFor="username">Shop Type</label>
            <input type="text" name="text" placeholder="Education" value={shopType}
                   onChange={e => setShopType(e.target.value)}/>
            {isLoading && <span>Loading...</span>}
            {isFailure && <span style={{color: "red"}}>Error</span>}
            <button type="button" className="optionsButton" onClick={() => updateShop(shopName)}>Update</button>
        </div>
    )
}

export default UpdateShop
