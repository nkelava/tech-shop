import { maxLength, minValue, numeric, required, url } from "@vuelidate/validators";
import { slugWithNumbersFormat } from "@/helpers/slug";
import { nameWithWhitespace } from "@/helpers/nameValidator";

const productRules = {
  name: {
    required,
    nameWithWhitespace,
    maxLength: maxLength(128),
  },
  slug: { required, slugWithNumbersFormat, maxLength: maxLength(128) },
  summary: { required },
  description: { required },
  price: { required, numeric, minValue: minValue(0) },
  imageURL: { url },
  subcategory: { required },
};

export { productRules };
