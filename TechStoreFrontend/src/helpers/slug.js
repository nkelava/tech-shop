import { helpers } from "@vuelidate/validators";

export const slugFormat = helpers.withMessage(
  'Invalid format. Expected format: "slug" or "this-is-a-slug"',
  (value) => {
    if (!value) return false;
    const formatRegex = /^[a-z]+(-[a-z]+)*$/;
    return formatRegex.test(value);
  }
);

export const slugWithNumbersFormat = helpers.withMessage(
  'Invalid format. Expected format: "slug" or "slug123" or "example-slug-123"',
  (value) => {
    if (!value) return false;
    const formatRegex = /^[a-z0-9]+(-[a-z0-9]+)*$/;
    return formatRegex.test(value);
  }
);
