<script setup>
import { reactive, ref } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { useToast } from "vue-toastification";
import emailjs from "@emailjs/browser";
import BaseInput from "@/components/common/BaseInput.vue";
import { initialContactState, contactRules } from "@/vuelidate/contact";
import { CONTACT_SUCCESS, CONTACT_FAIL } from "@/constants/messages/contact.js";

const props = defineProps({
  density: {
    type: String,
    default: "compact",
  },
  isBtnAbsolute: {
    type: Boolean,
    default: false,
  },
});
const formRef = ref(null);
const toast = useToast();
const contactState = reactive({ ...initialContactState });

const v$ = useVuelidate(contactRules, contactState);

const sendEmail = async () => {
  v$.value.$touch();

  if (v$.value.$pending) {
    toast.info("Please wait while we validate the form.");
    return;
  }

  if (v$.value.$invalid) {
    toast.error("Please fix the errors in the form before submitting.");
    return;
  }

  try {
    await emailjs.sendForm("service_k3zjinq", "template_njdddeo", formRef.value, {
      publicKey: "gAZ_t_BaWVRyr4qMu",
    });
    toast.success(CONTACT_SUCCESS);
    resetForm();
  } catch (error) {
    toast.error(CONTACT_FAIL);
    console.error("Failed to send email:", error);
  }
};

const resetForm = () => {
  v$.value.$reset();
  Object.assign(contactState, initialContactState);
};
</script>

<template>
  <div>
    <form ref="formRef" class="contact__form" @submit.prevent="sendEmail">
      <base-input
        v-model="contactState.name"
        class="contact__input"
        name="name"
        label="Name"
        :v$="v$.name"
        :density="props.density"
      />
      <base-input
        v-model="contactState.email"
        class="contact__input"
        name="email"
        label="Email"
        :v$="v$.email"
        :density="props.density"
      />
      <base-input
        v-model="contactState.subject"
        class="contact__input"
        name="subject"
        label="Subject"
        :v$="v$.subject"
        :density="props.density"
      />
      <v-textarea
        v-model="contactState.message"
        :error-messages="v$.message.$errors.map((e) => e.$message)"
        class="contact__input"
        name="message"
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
  </div>
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
  z-index: 100000;
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
