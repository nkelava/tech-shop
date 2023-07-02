import { required, email } from "@vuelidate/validators";

const rules = {
  email: { required, email },
  password: { required },
};

export { rules };
