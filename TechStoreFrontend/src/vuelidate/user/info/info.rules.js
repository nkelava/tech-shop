import { required, alpha } from "@vuelidate/validators";

const infoRules = {
  firstName: { required, alpha },
  lastName: { required, alpha },
};

export { infoRules };
