<script setup>
import { computed, ref } from "vue";
import { reactive } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { initialState as initPaymentState, rules as paymentRules } from "@/vuelidate/payment";
import { initialState as initDeliveryState, rules as deliveryRules } from "@/vuelidate/delivery";

const hasDeliveryAddress = ref(false);

const paymentState = reactive({
  ...initPaymentState,
});

const deliveryState = reactive({
  ...initDeliveryState,
});

const paymentValidationRules = computed(() => paymentRules);
const deliveryValidationRules = computed(() => deliveryRules);

const vp$ = useVuelidate(paymentValidationRules, paymentState);
const vd$ = useVuelidate(deliveryValidationRules, deliveryState);

const handleSubmit = async () => {
  const isPaymentFormValid = await vp$.value.$validate();

  if (!isPaymentFormValid) {
    alert("Not Submitted!");
    return;
  }

  if (hasDeliveryAddress.value) {
    const isDeliveryFormValid = await vd$.value.$validate();

    if (!isDeliveryFormValid) {
      alert("Not Submitted!");
      return;
    }
  }

  alert("Submitted!");
  clearForm(vp$, paymentState, initPaymentState);
  clearForm(vd$, deliveryState, initDeliveryState);
};

const clearForm = (form, formState, initialFormState) => {
  form.value.$reset();

  for (const [key, value] of Object.entries(initialFormState)) {
    formState[key] = value;
  }
};
</script>

<template>
  <v-container class="my-4">
    <h2 class="display-2 header mb-4">Basket checkout</h2>

    <form fast-fail @submit.prevent="handleSubmit">
      <v-row no-gutters>
        <v-col class="mr-5">
          <h3 class="mb-2">Payment address</h3>
          <v-row>
            <v-col>
              <v-text-field
                class="app-combobox"
                v-model="paymentState.firstName"
                :error-messages="vp$.firstName.$errors.map((e) => e.$message)"
                label="First Name"
              />
            </v-col>
            <v-col>
              <v-text-field
                v-model="paymentState.lastName"
                :error-messages="vp$.lastName.$errors.map((e) => e.$message)"
                label="Last Name"
              />
            </v-col>
          </v-row>
          <v-text-field
            v-model="paymentState.email"
            :error-messages="vp$.email.$errors.map((e) => e.$message)"
            label="E-mail"
          />
          <v-text-field
            v-model="paymentState.address"
            :error-messages="vp$.address.$errors.map((e) => e.$message)"
            label="Address"
          />
          <v-row>
            <v-col>
              <v-text-field
                v-model="paymentState.city"
                :error-messages="vp$.city.$errors.map((e) => e.$message)"
                label="City"
              />
            </v-col>
            <v-col>
              <v-text-field
                v-model="paymentState.zipCode"
                :error-messages="vp$.zipCode.$errors.map((e) => e.$message)"
                label="Zip Code"
              />
            </v-col>
          </v-row>
          <v-text-field
            v-model="paymentState.country"
            :error-messages="vp$.country.$errors.map((e) => e.$message)"
            label="Country"
          />
          <v-text-field
            v-model="paymentState.phone"
            :error-messages="vp$.phone.$errors.map((e) => e.$message)"
            label="Phone"
          />

          <v-checkbox v-model="hasDeliveryAddress" label="Orther delivery address"></v-checkbox>
        </v-col>
        <v-col>
          <h3 class="mb-2">Delivery address</h3>
          <div v-if="hasDeliveryAddress">
            <v-row>
              <v-col>
                <v-text-field
                  v-model="deliveryState.firstName"
                  :error-messages="vd$.firstName.$errors.map((e) => e.$message)"
                  label="First Name"
                />
              </v-col>
              <v-col>
                <v-text-field
                  v-model="deliveryState.lastName"
                  :error-messages="vd$.lastName.$errors.map((e) => e.$message)"
                  label="Last Name"
                />
              </v-col>
            </v-row>
            <v-text-field
              v-model="deliveryState.address"
              :error-messages="vd$.address.$errors.map((e) => e.$message)"
              label="Address"
            />
            <v-row>
              <v-col>
                <v-text-field
                  v-model="deliveryState.city"
                  :error-messages="vd$.city.$errors.map((e) => e.$message)"
                  label="City"
                />
              </v-col>
              <v-col>
                <v-text-field
                  v-model="deliveryState.zipCode"
                  :error-messages="vd$.zipCode.$errors.map((e) => e.$message)"
                  label="Zip Code"
                />
              </v-col>
            </v-row>
            <v-text-field
              v-model="deliveryState.country"
              :error-messages="vd$.country.$errors.map((e) => e.$message)"
              label="Country"
            />
          </div>
          <div v-else>
            <v-alert type="info" :value="true">
              The delivery address is the same as the payment address
            </v-alert>
          </div>
          <v-btn class="w-100 mt-3" color="success" type="submit"> Complete order </v-btn>
        </v-col>
      </v-row>
    </form>
  </v-container>
</template>

<style scoped>
.header {
  color: var(--ts-c-ternary);
}
</style>
