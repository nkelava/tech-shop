import { subcategoriesDb } from "../seed/subcategories.js";
import { getAttributesWithValues } from "./attributeService.js";

export function getSubcategoryBySlug(slug) {
  return subcategoriesDb.find((subcategory) => subcategory.slug === slug);
}

export function getSubcategoryAttributesWithValues(subcategoryId) {
  const subcategoryAttributesWithValues = getAttributesWithValues(subcategoryId);
  return subcategoryAttributesWithValues;
}

export function getSubcategoriesByCategoryId(categoryId) {
  return subcategoriesDb.filter((subcategory) => subcategory.categoryId === categoryId);
}
