import { attributesDb } from "../seed/attributes.js";
import { attributeValuesDb } from "../seed/attributeValues.js";
import { subcategoryAttributesDb } from "../seed/subcategoryAttributes.js";
import { getValueById } from "./valuesService.js";

export function getAttributesWithValues(subcategoryId) {
  const attributesWithValues = [];
  const attributeValueIds = subcategoryAttributesDb
    .filter((sa) => sa.subcategoryId === subcategoryId)
    .map((sa) => sa.attributeValuesId);

  attributeValuesDb.forEach((av) => {
    if (attributeValueIds.includes(av.id)) {
      const attribute = {
        ...getAttributeById(av.attributeId),
        values: getValueById(av.valueId),
      };
      attributesWithValues.push(attribute);
    }
  });

  console.log(attributesWithValues);
  return attributesWithValues;
}

export function getAttributeById(attributeId) {
  return attributesDb.find((attribute) => attribute.id === attributeId);
}
