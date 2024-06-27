import { maxLength, required } from "@vuelidate/validators";
import { slugWithNumbersFormat } from "@/helpers/slug";
import { nameWithWhitespace } from "@/helpers/nameValidator";

const subcategoryRules = {
  name: { required, nameWithWhitespace, maxLength: maxLength(48) },
  slug: { required, slugWithNumbersFormat, maxLength: maxLength(48) },
  category: { required },
};

export { subcategoryRules };
