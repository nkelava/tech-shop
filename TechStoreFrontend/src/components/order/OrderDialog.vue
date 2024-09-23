<script setup>
import { computed, ref, reactive } from "vue";
import { useToast } from "vue-toastification";
import { useVuelidate } from "@vuelidate/core";
import { axiosPrivate, axiosPublic } from "@/api/axios";
import { useCartStore } from "@/store";
import { initialState as initPaymentState, rules as paymentRules } from "@/vuelidate/payment";
import { initialState as initDeliveryState, rules as deliveryRules } from "@/vuelidate/delivery";
import BaseInput from "@/components/common/BaseInput.vue";

const dialog = ref(false);
const emit = defineEmits(["toggleDialog"]);
const cart = useCartStore();
const toast = useToast();
const hasDeliveryAddress = ref(false);
const paymentState = reactive({ ...initPaymentState });
const deliveryState = reactive({ ...initDeliveryState });
const paymentMethods = [
  {
    name: "Cash",
    value: 0,
  },
  {
    name: "Credit card",
    value: 1,
  },
];

const paymentValidationRules = computed(() => paymentRules);
const deliveryValidationRules = computed(() => deliveryRules);

const vp$ = useVuelidate(paymentValidationRules, paymentState);
const vd$ = useVuelidate(deliveryValidationRules, deliveryState);

const handleSubmit = async () => {
  if (!(await vp$.value.$validate())) return;

  if (hasDeliveryAddress.value) {
    if (!(await vd$.value.$validate())) return;
  }

  const order = {
    ...paymentState,
    ...(hasDeliveryAddress.value ? { deliveryAddress: { ...deliveryState } } : {}),
    products: cart.formattedCartItemsForOrder,
  };

  try {
    await axiosPrivate
      .post("/orders", order)
      .then(async (resp) => {
        if (resp?.status !== 200) {
          toast.error("Uh-oh! There was an issue processing your order. Please try again.");
          return;
        }

        if (resp?.data?.redirectUrl) {
          window.location.href = resp.data.redirectUrl;
        }

        await cart.clearStore();

        if (cart.isUserLoggedIn) {
          const resp = await axiosPrivate.delete("/carts").catch((error) => console.log(error));

          if (resp?.status !== 200) {
            toast.error("Uh-oh! There was an issue while cleaning your cart. Please try again.");
            return;
          }
        }
      })
      .catch((error) => {
        toast.error("Uh-oh! There was an issue processing your order. Please try again.");
        console.log(error);
      });

    // TODO: check if cart is cleared on the backend
    await cart.clearStore();

    if (cart.isUserLoggedIn) {
      const resp = await axiosPrivate.delete("/carts").catch((error) => console.log(error));

      if (resp?.status !== 200) {
        toast.error("Uh-oh! There was an issue processing your order. Please try again.");
        return;
      }
    }

    toast.success("Order received! Thank you for choosing us.");
    resetForm(vp$, paymentState, initPaymentState);
    resetForm(vd$, deliveryState, initDeliveryState);
    emit("toggleDialog");
  } catch {
    toast.error("Uh-oh! There was an issue processing your order. Please try again.");
  }
};

const resetForm = (form, formState, initialFormState) => {
  form.value.$reset();
  Object.assign(formState, initialFormState);
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
                      label="First Name*"
                    />
                  </v-col>
                  <v-col>
                    <base-input
                      v-model="paymentState.lastName"
                      :v$="vp$.lastName"
                      label="Last Name*"
                    />
                  </v-col>
                </v-row>
                <base-input v-model="paymentState.email" :v$="vp$.email" label="E-mail*" />
                <base-input
                  v-model="paymentState.shippingAddress"
                  :v$="vp$.shippingAddress"
                  label="Address*"
                />
                <v-row>
                  <v-col>
                    <base-input v-model="paymentState.city" :v$="vp$.city" label="City*" />
                  </v-col>
                  <v-col>
                    <base-input
                      v-model="paymentState.zipCode"
                      :v$="vp$.zipCode"
                      label="Zip Code*"
                    />
                  </v-col>
                </v-row>
                <base-input v-model="paymentState.country" :v$="vp$.country" label="Country*" />
                <base-input
                  v-model="paymentState.contactNumber"
                  :v$="vp$.contactNumber"
                  label="Contact Number*"
                />
                <v-select
                  v-model="paymentState.paymentMethod"
                  class="mt-5 test"
                  name="paymentMethod"
                  label="Payment Method*"
                  :items="paymentMethods"
                  item-value="value"
                  item-title="name"
                  density="compact"
                  hide-details="auto"
                  variant="outlined"
                  :error-messages="vp$?.paymentMethod?.$errors.map((e) => e.$message)"
                />

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
                        label="First Name*"
                      />
                    </v-col>
                    <v-col>
                      <base-input
                        v-model="deliveryState.lastName"
                        :v$="vd$.lastName"
                        label="Last Name*"
                      />
                    </v-col>
                  </v-row>
                  <base-input
                    v-model="deliveryState.shippingAddress"
                    :v$="vd$.shippingAddress"
                    label="Address*"
                  />
                  <v-row>
                    <v-col>
                      <base-input v-model="deliveryState.city" :v$="vd$.city" label="City*" />
                    </v-col>
                    <v-col>
                      <base-input
                        v-model="deliveryState.zipCode"
                        :v$="vd$.zipCode"
                        label="Zip Code*"
                      />
                    </v-col>
                  </v-row>
                  <base-input v-model="deliveryState.country" :v$="vd$.country" label="Country*" />
                  <base-input
                    v-model="deliveryState.contactNumber"
                    :v$="vd$.contactNumber"
                    label="Contact Number*"
                  />
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
        <v-btn class="btn-action--cancel" variant="text" @click="closeDialog"> Back To Cart </v-btn>
        <v-btn class="btn-action--submit" variant="text" @click="handleSubmit"> Complete </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<style scoped>
.dialog {
  max-width: 740px;
  max-height: 940px;
}

.dialog__card {
  height: 100%;
  width: 100%;
  padding: 1rem 0.5rem;
  background: var(--ts-c-bg-dark);
}

.dialog__card > * {
  color: var(--ts-c-text-light);
}

.card__title {
  color: var(--ts-c-primary-darker);
  font-weight: bold;
}

.btn-action--cancel {
  color: var(--ts-c-danger) !important;
}

.btn-action--submit {
  color: var(--ts-c-success) !important;
}
</style>
