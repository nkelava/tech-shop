import { maxLength, required } from "@vuelidate/validators";
import { slugFormat } from "@/helpers/slug";
import { nameWithWhitespace } from "@/helpers/nameValidator";

const categoryRules = {
  name: { required, nameWithWhitespace, maxLength: maxLength(48) },
  slug: { required, slugFormat, maxLength: maxLength(48) },
};

export { categoryRules };
