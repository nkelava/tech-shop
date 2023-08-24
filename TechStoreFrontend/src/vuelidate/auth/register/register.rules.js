import { alpha, email, required } from "@vuelidate/validators";

const rules = {
  email: { required, email },
  password: { required },
};

export { rules };
