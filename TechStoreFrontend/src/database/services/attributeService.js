import { attributesDb } from "../seed/attributes.js";
import { attributeValuesDb } from "../seed/attributeValues.js";
import { subcategoryAttributesDb } from "../seed/subcategoryAttributes.js";
import { getValueById } from "./valuesService.js";

export function getAttributesWithValues(subcategoryId) {
  const attributeValueIds = subcategoryAttributesDb
    .filter((sa) => sa.subcategoryId === subcategoryId)
    .map((sa) => sa.attributeValuesId);

  const attributesWithValues = attributeValuesDb.reduce((attributes, currAttributeValue) => {
    if (attributeValueIds.includes(currAttributeValue.id)) {
      let attribute = attributes.find((attr) => attr.id === currAttributeValue.attributeId);

      if (attribute) {
        attribute.values.push(getValueById(currAttributeValue.valueId));
      } else {
        attribute = getAttributeById(currAttributeValue.attributeId);
        attribute.values = [getValueById(currAttributeValue.valueId)];
        attributes.push(attribute);
      }
    }
    return attributes;
  }, []);

  return attributesWithValues;
}

export function getAttributeById(attributeId) {
  return attributesDb.find((attribute) => attribute.id === attributeId);
}
