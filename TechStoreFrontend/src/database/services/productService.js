import { productsDb } from "../seed/products.js";

export function getProductsBySubcategoryId(subcategoryId) {
  return productsDb.filter((product) => product.subcategoryId === subcategoryId);
}

export function getProducts() {
  return productsDb;
}
