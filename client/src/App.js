const products = [
  { name: "product 1", price: 100 },
  { name: "product 2", price: 200 },
];

function App() {
  return (
    <div>
      <h1 style={{ color: "red" }}>Re-Store</h1>
      <ul>
        {products.map((product) => (
          <li>
            {product.name}: {product.price}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
