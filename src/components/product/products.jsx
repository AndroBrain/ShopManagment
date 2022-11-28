import React, {useEffect, useState} from "react";
import {apiUrl, jwtKey} from "../../App";
import {useNavigate, useParams} from "react-router-dom";

function Products() {
    let pageSize = 5
    let params = useParams()
    const [allProducts, setAllProducts] = useState([])
    const [products, setProducts] = useState([])
    const [page, setPage] = useState(1)

    const navigate = useNavigate()

    useEffect(() => {
        let xhr = new XMLHttpRequest();
        let url = apiUrl + "/product/GetByShop?shopId=" + params.id;

        xhr.open("GET", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.setRequestHeader("Authorization", 'Bearer ' + localStorage.getItem(jwtKey))
        xhr.onreadystatechange = () => {
            if (xhr.readyState === 4) {
                console.log(xhr.status)
                if (xhr.status >= 200 && xhr.status < 300) {
                    console.log(xhr.responseText)
                    const allProducts = JSON.parse(xhr.responseText)
                    setAllProducts(allProducts)
                    changeProducts(page, allProducts)
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
    }, [navigate])

    const changeProducts = (currentPage, pagingProducts) => {
        setPage(currentPage)
        setProducts(pagingProducts.slice((currentPage - 1) * pageSize, currentPage * pageSize))
    }

    return (
        <div className="shopContainer">
            <h1>Products of {params.name}</h1>
            {products.length === 0 && <h3>This shop doesn't have any products yet</h3>}
            <button type="button" className="optionsButton"
                    onClick={() => navigate(`/products/add/${params.id}`)}>Add New Product
            </button>
            <ul className="productList">
                {products.map(product => (
                    <li key={product.id} className="shopItem">
                        <div>
                            <p className="name">Name: {product.name}</p>
                        </div>
                        <div>
                            <p className="name">Price: {product.price}</p>
                        </div>
                    </li>
                ))}
            </ul>
            {page > 1 && <button type="button" className="optionsButton"
                                 onClick={() => changeProducts(page - 1, allProducts)}>Previous Page
            </button>}
            {allProducts.length > page * pageSize && <button type="button" className="optionsButton"
                                                             onClick={() => changeProducts(page + 1, allProducts)}>Next
                Page
            </button>}
        </div>
    )
}

export default Products;
