export function filterProducts(products, price, rating, filters) {
  const filterAttributes = Array.from(filters.keys());
  let checkAttributeValue = true;
  let checkRating = true;
  let checkPrice = true;

  const filteredProducts = products.filter((product) => {
    if (filterAttributes.length) {
      checkAttributeValue = filterAttributes.some((attrId) =>
        Array.from(filters.get(attrId)).some((attrValue) =>
          product.productAttributes.some((attr) => attr.attributeValueId === attrValue)
        )
      );
    }

    if (rating != 0) {
      checkRating = product.rating >= rating;
    }

    checkPrice = product.price >= price.from;

    if (price.to > 0) {
      checkPrice = product.price <= price.to;
    }

    return checkAttributeValue && checkRating && checkPrice;
  });

  return filteredProducts;
}

export default {
  filterProducts,
};
