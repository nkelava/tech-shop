<script setup>
import { ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import { useToast } from "vue-toastification";
import "vue-toast-notification/dist/theme-sugar.css";

const props = defineProps(["product"]);
const dialog = ref(false);
const rating = ref(1);
const message = ref("");
const toast = useToast();
const emit = defineEmits(["toggleDialog"]);

const handleSubmit = async () => {
  if (message.value.length < 1) return;

  try {
    await axiosPrivate.post("/reviews", {
      Rate: rating.value,
      Comment: message.value,
      ProductId: props.product.id,
    });

    rating.value = 1;
    message.value = "";
    toast.success("Your review has been added successfully.");
    emit("toggleDialog");
  } catch {
    toast.error("Failed to add your review. Please try again later.");
  }
};

const closeDialog = () => {
  emit("toggleDialog");
};
</script>

<template>
  <v-dialog class="dialog" v-model="dialog">
    <v-card class="dialog__card">
      <v-card-title class="card__title"> Leave your review </v-card-title>
      <v-container class="mb-4">
        <v-divider></v-divider>
        <form fast-fail @submit.prevent>
          <v-row no-gutters>
            <v-col class="mr-5">
              <v-card-text>
                <v-row class="rating">
                  <h3>Rating:</h3>
                  <v-rating v-model="rating" density="compact" />
                </v-row>
                <v-row>
                  <v-textarea
                    v-model="message"
                    class="review__textarea"
                    label="Your review"
                    variant="outlined"
                    prepend-inner-icon="mdi-comment"
                    hide-details="true"
                    no-resize
                    clearable
                    @keydown.enter.prevent
                  />
                </v-row>
              </v-card-text>
            </v-col>
          </v-row>
        </form>
      </v-container>
      <v-card-actions class="justify-space-between">
        <v-btn color="error" variant="text" @click="closeDialog"> Cancel </v-btn>
        <v-btn color="success" variant="text" @click="handleSubmit"> Submit </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<style scoped>
.dialog {
  max-width: 740px;
  max-height: 600px;
}

.dialog__card {
  height: 100%;
  width: 100%;
  padding: 0.5rem;
  background: var(--ts-c-bg-light);
}

.dialog__card > * {
  color: var(--ts-c-text-dark);
}

.card__title {
  font-weight: bold;
  color: var(--ts-c-primary);
}

.rating {
  display: flex;
  justify-content: flex-start;
  align-items: center;
  gap: 0.5rem;
  margin-bottom: 1rem;
}
</style>
