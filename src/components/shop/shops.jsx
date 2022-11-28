import React, {useEffect, useState} from "react";
import {apiUrl, jwtKey} from "../../App";
import {Link, useNavigate} from "react-router-dom";
import "./shopstyle.scss"

function Shops() {
    const [shops, setShops] = useState([])
    const [filter, setFilter] = useState("")

    const navigate = useNavigate()

    function getShops() {
        let xhr = new XMLHttpRequest();
        let url = apiUrl + "/Shop/GetAll";

        xhr.open("GET", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.setRequestHeader("Authorization", 'Bearer ' + localStorage.getItem(jwtKey))
        xhr.onreadystatechange = () => {
            if (xhr.readyState === 4) {
                console.log(xhr.status)
                if (xhr.status >= 200 && xhr.status < 300) {
                    const responseShops = JSON.parse(xhr.responseText);
                    setShops(responseShops.filter(shop => shop.name.includes(filter)))
                } else {
                    console.log(xhr.status + '\n' + xhr.responseText)
                    if (xhr.status === 401) {
                        localStorage.removeItem(jwtKey)
                        navigate('/')
                    }
                }
            }
        };
        xhr.send();
    }

    useEffect(() => {
        getShops()
    }, [navigate])

    const removeShop = (id) => {
        let xhr = new XMLHttpRequest();
        let url = apiUrl + "/Shop/Delete?shopId=" + id;

        xhr.open("Delete", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.setRequestHeader("Authorization", 'Bearer ' + localStorage.getItem(jwtKey))
        xhr.onreadystatechange = () => {
            if (xhr.readyState === 4) {
                console.log(xhr.status)
                if (xhr.status >= 200 && xhr.status < 300) {
                    setShops((prevState) => prevState.filter(function (value) {
                        return value.id !== id
                    }))
                } else {
                    console.log("id " + id)
                    console.log(xhr.status + '\n' + xhr.responseText)
                    if (xhr.status === 401) {
                        localStorage.removeItem(jwtKey)
                        navigate('/')
                    }
                }
            }
        };
        xhr.send();
    }

    return (
        <div className="shopContainer">
            <h1>Shops</h1>
            <Link to="/shops/add">Add New Shop</Link>
            {(filter.length === 0 && shops.length === 0) && <h3>You don't have any shops added yet</h3>}
            <div>
                <input type="text" name="text" placeholder="Shop Name"
                       onChange={e => setFilter(e.target.value)}/>
                <button type="button" className="optionsButton" onClick={() => getShops()}>Filter</button>
            </div>
            <ul className="shopList">
                {shops.map(shop => (
                    <li key={shop.id} className="shopItem">
                        <div className="flexBox">
                            <p className="name">{shop.name}</p>
                            <button type="button" className="optionsButton"
                                    onClick={() => navigate(`/products/${shop.id}&${shop.name}`)}>Show Shop Products
                            </button>
                            <button type="button" className="optionsButton"
                                    onClick={() => navigate(`/shops/edit/${shop.id}&${shop.name}&${shop.type.name}`)}>Edit
                            </button>
                            <button type="button" className="optionsButton" onClick={() => removeShop(shop.id)}>Delete
                            </button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    )
}

export default Shops;