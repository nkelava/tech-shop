import { alpha, email, required } from "@vuelidate/validators";

const rules = {
  firstName: { required, alpha },
  lastName: { required, alpha },
  email: { required, email },
  password: { required },
};

export { rules };
