<script setup>
import { computed, ref, reactive } from "vue";
import BaseInput from "@/components/common/BaseInput.vue";
import { useVuelidate } from "@vuelidate/core";
import { initialState as initPaymentState, rules as paymentRules } from "@/vuelidate/payment";
import { initialState as initDeliveryState, rules as deliveryRules } from "@/vuelidate/delivery";

const dialog = ref(false);
const emit = defineEmits(["toggleDialog"]);
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
  emit("toggleDialog");
};

const clearForm = (form, formState, initialFormState) => {
  form.value.$reset();

  for (const [key, value] of Object.entries(initialFormState)) {
    formState[key] = value;
  }
};

const closeDialog = () => {
  emit("toggleDialog");
};
</script>

<template>
  <v-dialog class="dialog" v-model="dialog">
    <v-card class="dialog__card">
      <v-card-title class="card__title"> Your Order Details</v-card-title>
      <v-container class="my-4">
        <form fast-fail @submit.prevent>
          <v-row no-gutters>
            <v-col class="mr-5">
              <v-card-text>
                <h3 class="mb-5">Payment address</h3>
                <v-row>
                  <v-col>
                    <base-input
                      v-model="paymentState.firstName"
                      :v$="vp$.firstName"
                      label="First Name"
                    />
                  </v-col>
                  <v-col>
                    <base-input
                      v-model="paymentState.lastName"
                      :v$="vp$.lastName"
                      label="Last Name"
                    />
                  </v-col>
                </v-row>
                <base-input v-model="paymentState.email" :v$="vp$.email" label="E-mail" />
                <base-input v-model="paymentState.address" :v$="vp$.address" label="Address" />
                <v-row>
                  <v-col>
                    <base-input v-model="paymentState.city" :v$="vp$.city" label="City" />
                  </v-col>
                  <v-col>
                    <base-input v-model="paymentState.zipCode" :v$="vp$.zipCode" label="Zip Code" />
                  </v-col>
                </v-row>
                <base-input v-model="paymentState.country" :v$="vp$.country" label="Country" />
                <base-input v-model="paymentState.phone" :v$="vp$.phone" label="Phone" />

                <v-checkbox
                  v-model="hasDeliveryAddress"
                  label="Other delivery address"
                  hide-details="auto"
                />
              </v-card-text>
              <v-card-text>
                <h3 class="mb-5">Delivery address</h3>
                <div v-if="hasDeliveryAddress">
                  <v-row>
                    <v-col>
                      <base-input
                        v-model="deliveryState.firstName"
                        :v$="vd$.firstName"
                        label="First Name"
                      />
                    </v-col>
                    <v-col>
                      <base-input
                        v-model="deliveryState.lastName"
                        :v$="vd$.lastName"
                        label="Last Name"
                      />
                    </v-col>
                  </v-row>
                  <base-input v-model="deliveryState.address" :v$="vd$.address" label="Address" />
                  <v-row>
                    <v-col>
                      <base-input v-model="deliveryState.city" :v$="vd$.city" label="City" />
                    </v-col>
                    <v-col>
                      <base-input
                        v-model="deliveryState.zipCode"
                        :v$="vd$.zipCode"
                        label="Zip Code"
                      />
                    </v-col>
                  </v-row>
                  <base-input v-model="deliveryState.country" :v$="vd$.country" label="Country" />
                </div>
                <div v-else>
                  <v-alert type="info" :value="true">
                    The delivery address is the same as the payment address
                  </v-alert>
                </div>
              </v-card-text>
            </v-col>
          </v-row>
        </form>
      </v-container>
      <v-card-actions class="justify-space-between">
        <v-btn color="error" variant="text" @click="closeDialog"> Back To Cart </v-btn>
        <v-btn color="success" variant="text" @click="handleSubmit"> Complete Order </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<style scoped>
.dialog {
  max-width: 740px;
  max-height: 905px;
}

.dialog__card {
  background: var(--ts-c-bg-dark);
  height: 100%;
  width: 100%;
}

.dialog__card > * {
  color: var(--ts-c-text-light);
}

.card__title {
  color: var(--ts-c-primary-darker);
  font-weight: bold;
}
</style>
