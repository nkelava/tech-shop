import { specificationsDb } from "../seed/specifications.js";
import { getAttributeById } from "./attributeService.js";
import { getValueById } from "./valuesService.js";

export function getSpecificationByProductId(productId) {
  let attribute = {};
  let value = {};
  const productSpecification = {};
  const specifications = specificationsDb.filter((pav) => pav.productId === productId);

  if (!specifications.length) return productSpecification;

  specifications.forEach((pav) => {
    attribute = getAttributeById(pav.attributeId);
    value = getValueById(pav.valueId);
    productSpecification[attribute.name] = value;
  });

  return productSpecification;
}
