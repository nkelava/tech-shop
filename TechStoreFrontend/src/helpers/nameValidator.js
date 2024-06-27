import { helpers } from "@vuelidate/validators";

export const nameWithWhitespace = helpers.withMessage(
  'Invalid format. Expected format: "name" or "Name example 123"',
  (value) => {
    if (!value) return false;
    const formatRegex = /^[a-z0-9A-z_ ]*$/i;
    return formatRegex.test(value);
  }
);
