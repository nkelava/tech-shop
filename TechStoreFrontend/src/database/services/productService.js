import { productsDb } from "../seed/products.js";
import { getSpecificationByProductId } from "./specificationService.js";

export function getProducts() {
  return productsDb;
}

export function getProductById(productId) {
  return productsDb.find((product) => product.id === productId);
}

export function getProductsByTitle(searchTerm) {
  return productsDb.filter((product) =>
    product.title.toLowerCase().includes(searchTerm.toLowerCase())
  );
}

export function getProductsBySubcategoryId(subcategoryId) {
  return productsDb.filter((product) => product.subcategoryId === subcategoryId);
}

export function getFilteredProductsBySubcategoryId(subcategoryId, filters) {
  const filterAttributes = Object.keys(filters);
  const products = getProductsBySubcategoryId(subcategoryId);

  if (!filterAttributes.length) return products;

  const productsWithSpecification = getProductsWithSpecification(products);
  const filteredProducts = productsWithSpecification.filter((product) =>
    filterAttributes.every((attribute) =>
      filters[attribute].has(product.specification[attribute].value)
    )
  );

  return filteredProducts;
}

export function getProductsWithSpecification(products) {
  const productsWithSpecifications = [];

  products.forEach((product) => {
    const specification = getSpecificationByProductId(product.id);

    if (Object.keys(specification).length) {
      product.specification = specification;
      productsWithSpecifications.push(product);
    }
  });

  return productsWithSpecifications;
}

export function getProductWithSpecification(productId) {
  const product = getProductById(productId);
  product.specification = getSpecificationByProductId(productId);
  return product;
}
