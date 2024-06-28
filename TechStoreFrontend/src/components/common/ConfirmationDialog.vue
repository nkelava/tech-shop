<script setup>
import { ref, watch } from "vue";

const props = defineProps({
  title: String,
  content: String,
  confirmText: String,
  showDialog: Boolean,
  itemId: [String, Number],
});
const emits = defineEmits(["update:showDialog", "confirm"]);
const dialog = ref(props.showDialog);

const closeDialog = () => {
  emits("update:showDialog", false);
};

const confirmDelete = () => {
  emits("confirm", props.itemId);
  closeDialog();
};

watch(
  () => props.showDialog,
  (newVal) => {
    dialog.value = newVal;
  }
);
</script>

<template>
  <v-dialog v-model="dialog" max-width="500">
    <v-card class="dialog">
      <v-card-title class="headline">{{ title }} </v-card-title>
      <v-card-text class="mb-3"> {{ content }} </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="grey" text @click="closeDialog">Cancel</v-btn>
        <v-btn color="red darken-1" text @click="confirmDelete">{{ confirmText }}</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<style scoped>
.dialog {
  padding: 1rem 1rem 0.5rem;
  color: var(--ts-c-text-dark);
  background: var(--ts-c-bg-light);
}

.headline {
  color: var(--ts-c-primary-dark);
}
.v-card-title {
  justify-content: center;
}
.v-card-actions {
  justify-content: flex-end;
}
</style>
