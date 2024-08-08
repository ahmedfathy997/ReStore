import { useEffect } from "react";
import LoadingComponents from "../../app/layout/LoadingComponents";
import { fetchProductsAsync, productSelectors } from "./catalogSlice";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import ProductList from "./ProductList";

export default function Catalog() {
  const products = useAppSelector(productSelectors.selectAll);
  const { productLoadedState, status} = useAppSelector(state => state.catalog);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if(!productLoadedState) dispatch(fetchProductsAsync());
  }, [productLoadedState, dispatch]);

  if(status.includes("pendingFetchingProducts")) return <LoadingComponents message="Loading Products..."/>

  return <ProductList products={products} />;
}
