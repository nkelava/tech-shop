<script setup>
import { computed, reactive, ref } from "vue";
import { useToast } from "vue-toastification";
import { axiosPublic } from "@/api/axios";
import { useVuelidate } from "@vuelidate/core";
import { email } from "@vuelidate/validators";
import BaseInput from "@/components/common/BaseInput.vue";

const toast = useToast();
const initialNewslettersState = { email: "" };
const rules = { email: { email } };
const newslettersState = reactive({ ...initialNewslettersState });
const v$ = useVuelidate(rules, newslettersState);
const isFieldShort = computed(() => newslettersState.email.length < 4);

async function onSubscribe() {
  if (!(await v$.value.$validate())) return;

  await axiosPublic
    .post("/newsletters", { email: newslettersState.email })
    .then(() => {
      toast.success(
        "Thank you for subscribing to our newsletter! You will now receive the latest updates and exclusive offers directly in your inbox."
      );
      resetForm();
    })
    .catch((error) => {
      console.log(error);
      toast.error(
        "Oops! There was an issue with your subscription. Please try again later or contact our support team for assistance."
      );
    });
}

function resetForm() {
  v$.value.$reset();
  Object.assign(newslettersState, initialNewslettersState);
}
</script>

<template>
  <div class="newsletters ts-container">
    <div class="newsletters__heading">
      <h2>Subscribe to our newsletters!</h2>
      <h4>Get early access to new tech products and sales.</h4>
    </div>

    <div class="newsletters__subscribe">
      <base-input
        v-model="newslettersState.email"
        class="subscribe__input"
        label="Email"
        :v$="v$.email"
        variant="filled"
        density="compact"
      />
      <input type="submit" value="Subscribe" @click="onSubscribe" :disabled="isFieldShort" />
    </div>
  </div>
</template>

<style scoped>
.newsletters {
  display: grid;
  gap: 5rem;
  grid-template-areas: "heading input";
  background-color: var(--ts-c-bg-highlight);
}

.newsletters:nth-child(1) {
  grid-area: heading;
}

.newsletters:nth-child(2) {
  grid-area: input;
}

.newsletters__heading {
  color: var(--ts-c-text-dark);
  text-align: end;
}

.newsletters__heading h4 {
  color: var(--ts-c-primary-soft);
}

.newsletters__subscribe {
  position: relative;
  display: flex;
  align-items: start;
  align-self: center;
  justify-self: start;
}

.subscribe__input {
  width: 40em;
  margin-bottom: 0 !important;
  background-color: var(--ts-c-white) !important;
  color: var(--ts-c-text-dark);
  border-radius: 5px;
  font-weight: bold;
}

input[type="submit"] {
  background-color: var(--ts-c-bg-dark);
  border-radius: 5px;
  color: var(--ts-c-text-light);
  font-weight: bold;
  height: 2.5rem;
  width: 7rem;
  position: absolute;
  right: 3px;
  top: 3px;
}

input[type="submit"]:hover {
  background-color: var(--ts-c-primary-dark);
}

@media only screen and (max-width: 1024px) {
  .newsletters {
    grid-template-areas:
      "heading"
      "input";
    gap: 1rem;
    justify-content: center;
    align-items: center;
  }

  .newsletters__heading {
    text-align: center;
  }

  .newsletters__subscribe,
  .newsletters__subscribe .subscribe__input {
    width: 100%;
  }
}
</style>
