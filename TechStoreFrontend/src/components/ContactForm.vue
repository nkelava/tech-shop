<script setup>
import { reactive } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { alpha, email, required } from "@vuelidate/validators";
import BaseInput from "@/components/BaseInput.vue";

const props = defineProps(["density", "isBtnAbsolute"]);
const initialContactState = {
  name: "",
  email: "",
  subject: "",
  message: "",
};

const contactRules = {
  name: { alpha, required },
  email: { email, required },
  subject: { required },
  message: { required },
};

const contactState = reactive({
  ...initialContactState,
});

const v$ = useVuelidate(contactRules, contactState);

async function onMessageSend() {
  const isFormValid = await v$.value.$validate();

  if (!isFormValid) {
    return;
  }

  alert("Success!");
  clearForm(v$, initialContactState, contactState);
}

const clearForm = (form, initialFormState, formState) => {
  form.value.$reset();

  for (const [key, value] of Object.entries(initialFormState)) {
    formState[key] = value;
  }
};
</script>

<template>
  <form class="contact__form" @submit.prevent="onMessageSend">
    <base-input
      v-model="contactState.name"
      class="contact__input"
      label="Name"
      :v$="v$.name"
      :density="props.density"
    />
    <base-input
      v-model="contactState.email"
      class="contact__input"
      label="Email"
      :v$="v$.email"
      :density="props.density"
    />
    <base-input
      v-model="contactState.subject"
      class="contact__input"
      label="Subject"
      :v$="v$.subject"
      :density="props.density"
    />
    <v-textarea
      v-model="contactState.message"
      :error-messages="v$.message.$errors.map((e) => e.$message)"
      class="contact__input"
      label="Message"
      hide-details="auto"
      variant="outlined"
      rows="7"
      cols="50"
      :density="props.density"
    />
    <input
      :class="props.isBtnAbsolute ? 'contact__btn--absolute' : 'contact__btn'"
      type="submit"
      value="Send"
    />
  </form>
</template>

<style scoped>
.contact__form {
  display: flex;
  flex-direction: column;
  position: relative;
}

.contact__btn {
  background-color: var(--ts-c-bg-highlight);
  border-radius: 5px;
  color: var(--ts-c-text-dark);
  font-weight: bold;
  margin-top: 1rem;
  padding: 8px 0;
}

.contact__btn--absolute {
  background: none !important;
  color: var(--ts-c-text-light) !important;
  position: absolute;
  bottom: 5px;
  right: 1rem;
  opacity: 0.5;
}

.contact__btn--absolute:hover {
  transform: scale(1.05);
  opacity: 1;
}
</style>
