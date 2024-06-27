import { alpha, email, minLength, required } from "@vuelidate/validators";

const rules = {
  firstName: { required, alpha },
  lastName: { required, alpha },
  email: { required, email },
  password: { required, minLength: minLength(8) },
};

export { rules };
