<script setup>
import { ref, watch } from "vue";

const props = defineProps({
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
    <v-card>
      <v-card-title class="headline">Confirm Deletion</v-card-title>
      <v-card-text> Are you sure you want to delete this item? </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="grey" text @click="closeDialog">Cancel</v-btn>
        <v-btn color="red darken-1" text @click="confirmDelete">Delete</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<style scoped>
.v-card-title {
  justify-content: center;
}
.v-card-actions {
  justify-content: flex-end;
}
</style>
