import { categoriesDb } from "../seed/categories.js";

export function getCategories() {
  return categoriesDb;
}

export function getCategoryBySlug(categorySlug) {
  return categoriesDb.find((category) => category.slug === categorySlug);
}
