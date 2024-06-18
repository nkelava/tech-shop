export function parseProductAttributes(products) {
  let attributeValuesMap = new Map();

  products.forEach((product) => {
    product.productAttributes.forEach((pa) => {
      if (attributeValuesMap.has(pa.attributeId)) {
        if (!attributeValuesMap.get(pa.attributeId)["values"].has(pa.attributeValueId)) {
          attributeValuesMap
            .get(pa.attributeId)
            ["values"].set(pa.attributeValueId, pa.attributeValue.value);
        }
      } else {
        attributeValuesMap.set(pa.attributeId, {
          name: `${pa.attribute.name}`,
          values: new Map([[pa.attributeValueId, pa.attributeValue.value]]),
        });
      }
    }, {});
  });

  return attributeValuesMap;
}

export default { parseProductAttributes };
