import { useState, useEffect } from "react";
import { Product } from "../../app/models/product"
import ProductList from "./ProductList";


export default function Catalog(){
  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    fetch('http://localhost:5225/api/product')
    .then(response => response.json())
    .then(data => setProducts(data))
  }, [])

    return(
      <ProductList products={products} />
    )
  }