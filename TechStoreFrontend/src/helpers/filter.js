export function filterProducts(products, price, rating, filters) {
  console.log("filters:", filters);
  const filterAttributes = Array.from(filters.keys());

  const filteredProducts = products.filter((product) => {
    let checkAttributeValue = true;
    let checkRating = true;
    let checkPrice = true;

    // Check attributes if there are filters selected
    if (filterAttributes.length > 0) {
      // Check if every selected attribute matches at least one product attribute
      checkAttributeValue = filterAttributes.every((attrId) =>
        Array.from(filters.get(attrId)).some((attrValue) =>
          product.productAttributes.some((attr) => attr.attributeValueId === attrValue)
        )
      );
    }

    // Check rating
    if (rating > 0) {
      checkRating = product.rating >= rating;
    }

    // Check price range
    if (price.from > 0 || price.to > 0) {
      if (price.from > 0) {
        checkPrice = product.price >= price.from;
      }
      if (price.to > 0) {
        checkPrice = checkPrice && product.price <= price.to;
      }
    }

    // Return the product if it passes all filters
    return checkAttributeValue && checkRating && checkPrice;
  });

  return filteredProducts;
}

export default {
  filterProducts,
};
