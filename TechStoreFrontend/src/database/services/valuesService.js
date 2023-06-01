import { valuesDb } from "../seed/values.js";

export function getValueById(valueId) {
  return valuesDb.find((value) => value.id === valueId);
}
