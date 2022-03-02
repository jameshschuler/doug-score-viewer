<template>
  <div class="field">
    <label class="label">{{ props.label }}</label>
    <div class="control">
      <div class="select is-fullwidth">
        <select :disabled="props.disabled">
          <option>Select {{ props.label }}</option>
          <option>With options</option>
        </select>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
const props = defineProps({
  label: String,
  disabled: Boolean,
});

function loadOptions() {
  console.log("loadOptions");
}
</script>
<style lang="scss"></style>
